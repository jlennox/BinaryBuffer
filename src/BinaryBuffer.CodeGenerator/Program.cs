using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BinaryBuffer.CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1 && args[0] == "--local")
            {
                GenerateForLocalProject();
                return;
            }

            var className = "BinaryBuffer";
            var namespaceName = "BinaryBuffer";

            if (args.Length == 2)
            {
                className = args[0];
                namespaceName = args[1];
            }

            var generator = new CodeGenerator();
            generator.GenerateClass(namespaceName, className);
            var code = generator.GetGenerateCode();
            Console.WriteLine(code);
        }

        private static void GenerateForLocalProject()
        {
            var currentSourceDirectory = Path.GetDirectoryName(GetSourcePath());
            var destinationPath = Path.GetFullPath(Path.Combine(
                currentSourceDirectory, "..",
                "BinaryBuffer", "BinaryBuffer.cs"));

            // If there's not at least a dummy file present, presume we're not
            // executing under the normal project structure and exit.
            if (!File.Exists(destinationPath))
            {
                Console.Error.WriteLine($"Unable to find '{destinationPath}', aborting.");
                Environment.Exit(-1);
            }

            var generator = new CodeGenerator();
            generator.GenerateClass("BinaryBuffer", "BinaryBuffer");
            var code = generator.GetGenerateCode();

            using (var fs = File.Open(destinationPath, FileMode.Truncate))
            {
                var bytes = Encoding.UTF8.GetBytes(code);
                fs.Write(bytes, 0, bytes.Length);
            }
        }

        private static string GetSourcePath([CallerFilePath]string path = "")
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(
                    nameof(path), "Unable to get path of source file.");
            }

            return path;
        }
    }

    class CodeGenerator
    {
        private readonly StringBuilder _code = new StringBuilder();

        private static readonly Dictionary<int, string>
            _lengthNameLookup = new Dictionary<int, string> {
                [1] = "byte",
                [2] = "short",
                [4] = "int",
                [8] = "long"
            };

        private static string MakeFirstUpperCase(string s)
        {
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        private void AppendLine(int indentDepth, string line)
        {
            _code.Append(' ', indentDepth * 4);
            _code.Append(line);
            _code.Append("\r\n");
        }

        private void AppendLine()
        {
            _code.Append("\r\n");
        }

        private static string NormalizeNewlines(string s)
        {
            return string.Join("\r\n", s.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None));
        }

        public void GenerateClass(string namespaceName, string className)
        {
            AppendLine(0, NormalizeNewlines($@"using System;
using System.Runtime.CompilerServices;

#pragma warning disable CS3002 // Return type is not CLS-compliant
namespace {namespaceName}
{{
    public unsafe struct {className}
    {{
        private readonly byte[] _buffer;
        private int _offset;

        public {className}(byte[] buffer, int offset)
            : this()
        {{
            if (buffer == null) {{ throw new ArgumentNullException(nameof(buffer)); }}
            if (offset < 0) {{ throw new ArgumentOutOfRangeException(nameof(offset), offset, ""Value must be positive.""); }}
            if (offset >= buffer.Length) {{ throw new ArgumentException(""Index out of range."", nameof(buffer)); }}

            _buffer = buffer;
            _offset = offset;
        }}

        public byte PeekByte()
        {{
            return _buffer[_offset];
        }}

        public byte ReadByte()
        {{
            var result = _buffer[_offset];
            ++_offset;
            return result;
        }}
"));

            foreach (var length in new[] { 2, 4, 8 })
            foreach (var unsigned in new[] { false, true })
            foreach (var littleEndian in new[] { true, false })
            {
                Generate(length, unsigned, littleEndian, 2);
                AppendLine();
            }

            AppendLine(1, $"}}");
            AppendLine(0, $"}}");
        }

        private void AppendBufferCheck(int length, int indentDepth)
        {
            AppendLine(indentDepth + 1, $"if (buffer == null) {{ throw new ArgumentNullException(nameof(buffer)); }}");
            AppendLine(indentDepth + 1, $"if (offset < 0) {{ throw new ArgumentOutOfRangeException(nameof(offset), offset, \"Value must be positive.\"); }}");
            AppendLine(indentDepth + 1, $"if (buffer.Length - offset < {length}) {{ throw new ArgumentException(\"Index out of range.\", nameof(buffer)); }}");
        }

        public void Generate(
            int length, bool unsigned,
            bool littleEndian, int indentDepth)
        {
            if (!_lengthNameLookup.TryGetValue(length, out var lengthName))
            {
                var error = "Invalid length. Must be one of " +
                    string.Join(", ", _lengthNameLookup.Keys);

                throw new ArgumentOutOfRangeException(
                    nameof(length), length, error);
            }

            var endianName = littleEndian ? "" : "Network";
            var printedLengthName = MakeFirstUpperCase(lengthName);

            if (unsigned)
            {
                printedLengthName = "U" + printedLengthName;
                lengthName = "u" + lengthName;
            }

            AppendLine(indentDepth + 0, $"public {lengthName} Peek{printedLengthName}{endianName}()");
            AppendLine(indentDepth + 0, $"{{");
            AppendLine(indentDepth + 1, $"fixed (byte* bufferPtr = &_buffer[_offset])");
            AppendLine(indentDepth + 1, $"{{");
            AppendRead(indentDepth, littleEndian, length, lengthName);
            AppendLine(indentDepth + 1, $"}}");
            AppendLine(indentDepth + 0, $"}}");
            AppendLine();

            AppendLine(indentDepth + 0, $"public {lengthName} Read{printedLengthName}{endianName}()");
            AppendLine(indentDepth + 0, $"{{");
            AppendLine(indentDepth + 1, $"var result = Peek{printedLengthName}{endianName}();");
            AppendLine(indentDepth + 1, $"_offset += {length};");
            AppendLine(indentDepth + 1, $"return result;");
            AppendLine(indentDepth + 0, $"}}");
            AppendLine();

            AppendLine(indentDepth + 0, $"[MethodImpl(MethodImplOptions.AggressiveInlining)]");
            AppendLine(indentDepth + 0, $"public static {lengthName} Read{printedLengthName}{endianName}(");
            AppendLine(indentDepth + 1, $"byte[] buffer, int offset)");
            AppendLine(indentDepth + 0, $"{{");
            AppendBufferCheck(length, indentDepth);
            AppendLine();
            AppendLine(indentDepth + 1, $"fixed (byte* bufferPtr = &buffer[offset])");
            AppendLine(indentDepth + 1, $"{{");
            AppendLine(indentDepth + 2, $"return Read{printedLengthName}{endianName}Unsafe(bufferPtr, offset);");
            AppendLine(indentDepth + 1, $"}}");
            AppendLine(indentDepth + 0, $"}}");
            AppendLine();

            AppendLine(indentDepth + 0, $"[MethodImpl(MethodImplOptions.AggressiveInlining)]");
            AppendLine(indentDepth + 0, $"public static {lengthName} Read{printedLengthName}{endianName}Unsafe(");
            AppendLine(indentDepth + 1, $"byte[] buffer, int offset)");
            AppendLine(indentDepth + 0, $"{{");
            AppendLine(indentDepth + 1, $"fixed (byte* bufferPtr = &buffer[offset])");
            AppendLine(indentDepth + 1, $"{{");
            AppendLine(indentDepth + 2, $"return Read{printedLengthName}{endianName}Unsafe(bufferPtr, offset);");
            AppendLine(indentDepth + 1, $"}}");
            AppendLine(indentDepth + 0, $"}}");
            AppendLine();

            AppendLine(indentDepth + 0, $"public static {lengthName} Read{printedLengthName}{endianName}Unsafe(");
            AppendLine(indentDepth + 1, $"byte* bufferPtr, int offset)");
            AppendLine(indentDepth + 0, $"{{");
            AppendRead(indentDepth, littleEndian, length, lengthName);
            AppendLine(indentDepth + 0, $"}}");

            AppendLine(indentDepth + 0, $"public void Write{printedLengthName}{endianName}({lengthName} i)");
            AppendLine(indentDepth + 0, $"{{");
            AppendLine(indentDepth + 1, $"Write{printedLengthName}{endianName}(_buffer, _offset, i);");
            AppendLine(indentDepth + 1, $"_offset += {length};");
            AppendLine(indentDepth + 0, $"}}");
            AppendLine();

            AppendLine(indentDepth + 0, $"[MethodImpl(MethodImplOptions.AggressiveInlining)]");
            AppendLine(indentDepth + 0, $"public static void Write{printedLengthName}{endianName}(");
            AppendLine(indentDepth + 1, $"byte[] buffer, int offset, {lengthName} i)");
            AppendLine(indentDepth + 0, $"{{");
            AppendBufferCheck(length, indentDepth);
            AppendLine();
            AppendLine(indentDepth + 1, $"fixed (byte* bufferPtr = &buffer[offset])");
            AppendLine(indentDepth + 1, $"{{");
            AppendLine(indentDepth + 2, $"Write{printedLengthName}{endianName}Unsafe(bufferPtr, offset, i);");
            AppendLine(indentDepth + 1, $"}}");
            AppendLine(indentDepth + 0, $"}}");
            AppendLine();

            AppendLine(indentDepth + 0, $"public static void Write{printedLengthName}{endianName}Unsafe(");
            AppendLine(indentDepth + 1, $"byte[] buffer, int offset, {lengthName} i)");
            AppendLine(indentDepth + 0, $"{{");
            AppendLine(indentDepth + 1, $"fixed (byte* bufferPtr = &buffer[offset])");
            AppendLine(indentDepth + 1, $"{{");
            AppendLine(indentDepth + 2, $"Write{printedLengthName}{endianName}Unsafe(bufferPtr, offset, i);");
            AppendLine(indentDepth + 1, $"}}");
            AppendLine(indentDepth + 0, $"}}");
            AppendLine();

            AppendLine(indentDepth + 0, $"public static void Write{printedLengthName}{endianName}Unsafe(");
            AppendLine(indentDepth + 1, $"byte* bufferPtr, int offset, {lengthName} i)");
            AppendLine(indentDepth + 0, $"{{");
            AppendWrite(indentDepth, littleEndian, length);
            AppendLine(indentDepth + 0, $"}}");
        }

        private void AppendRead(int indentDepth, bool littleEndian, int length, string lengthName)
        {
            var shiftAdjustment = littleEndian ? 8 : -8;
            var shiftAmount = littleEndian ? 0 : length * 8 - 8;
            var prefixed = length == 2 ? $"({lengthName})(" : "";
            var postfix = length == 2 ? $")" : "";
            AppendLine(indentDepth + 1, $"return {prefixed}(({lengthName})bufferPtr[0] << {shiftAmount}) |");
            shiftAmount += shiftAdjustment;
            for (var i = 0; i < length - 1; ++i)
            {
                var endswidth = i == length - 2 ? $"{postfix};" : " |";
                AppendLine(indentDepth + 2, $"(({lengthName})bufferPtr[{i + 1}] << {shiftAmount}){endswidth}");
                shiftAmount += shiftAdjustment;
            }
        }

        private void AppendWrite(int indentDepth, bool littleEndian, int length)
        {
            var shiftAdjustment = littleEndian ? 8 : -8;
            var writeShiftAmount = littleEndian ? 0 : length * 8 - 8;
            for (var i = 0; i < length; ++i)
            {
                AppendLine(indentDepth + 1, $"bufferPtr[{i}] = (byte)((i >> {writeShiftAmount}) & 0xFF);");
                writeShiftAmount += shiftAdjustment;
            }
        }

        public string GetGenerateCode()
        {
            return _code.ToString();
        }
    }
}
