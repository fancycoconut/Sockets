using Sockets.Core.Conversion;
using System.Text;

namespace Sockets.Coap
{
    public class CoapOption
    {
        public Option Number { get; set; }

        public byte[] RawValue { get; private set; }
        public int ValueLength => RawValue.Length;
        public OptionValueFormat ValueFormat { get; private set; }

        public bool Critical => ((int)Number & 0x1) == 1;
        public bool UnSafe => ((int)Number & 0x2) == 2;
        public bool NoCacheKey => ((int)Number & 0x1E) == 0x1C;

        // TODO validate what option can use what types 
        public CoapOption(Option number)
        {
            Number = number;
            RawValue = new byte[] { };
            ValueFormat = OptionValueFormat.Empty;
        }

        public CoapOption(Option number, byte[] value)
        {
            Number = number;
            RawValue = value;
            ValueFormat = OptionValueFormat.Opaque;
        }

        public CoapOption(Option number, string value)
        {
            Number = number;
            RawValue = Encoding.UTF8.GetBytes(value);
            ValueFormat = OptionValueFormat.String;
        }

        public CoapOption(Option number, uint value)
        {
            Number = number;
            RawValue = EndianBitConverter.Big.GetBytes(value);
            ValueFormat = OptionValueFormat.Uint;
        }

        public void SetValue(byte[] value)
        {
            ValueFormat = OptionValueFormat.Opaque;
            RawValue = value;
        }

        public void SetValue(string value)
        {
            ValueFormat = OptionValueFormat.String;
            RawValue = Encoding.UTF8.GetBytes(value);
        }

        public void SetValue(uint value)
        {
            ValueFormat = OptionValueFormat.Uint;
            RawValue = EndianBitConverter.Big.GetBytes(value);
        }
    }
}
