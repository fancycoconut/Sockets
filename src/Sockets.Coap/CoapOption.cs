using Sockets.Coap.Exceptions;
using Sockets.Core.Conversion;
using System.Collections.Generic;
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
            ValueFormat = OptionValueFormat.UInt;
        }

        public CoapOption(Option number, int value) : this(number, (uint)value) { }

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
            ValueFormat = OptionValueFormat.UInt;
            RawValue = EndianBitConverter.Big.GetBytes(value);
        }

        public void SetValue(int value)
        {
            SetValue((uint)value);
        }

        public static CoapOption FromNumberValue(int number, byte[] value)
        {
            var optionNumber = (Option)number;
            var validOption = optionValueFormats.TryGetValue(optionNumber, out var valueFormat);
            if (!validOption) throw new CoapOptionException($"Cannot get option value format for the specified option number #{number}");

            var option = new CoapOption(optionNumber);
            switch (valueFormat)
            {
                case OptionValueFormat.Opaque:
                    return new CoapOption(optionNumber, value);
                case OptionValueFormat.String:
                    var stringValue = value.Length == 0 ? string.Empty : Encoding.UTF8.GetString(value);
                    return new CoapOption(optionNumber, stringValue);
                case OptionValueFormat.UInt:
                    var uintValue = value.Length == 0 ? 0 : EndianBitConverter.Big.ToUInt32(value, 0);
                    return new CoapOption(optionNumber, uintValue);
                case OptionValueFormat.Empty:
                default:
                    break;
            }

            return option;
        }

        private static readonly Dictionary<Option, OptionValueFormat> optionValueFormats = new Dictionary<Option, OptionValueFormat>
        {
            // As per RFC7252 section 5.10
            { Option.IfMatch, OptionValueFormat.Opaque },
            { Option.UriHost, OptionValueFormat.String },
            { Option.ETag, OptionValueFormat.Opaque },
            { Option.IfNoneMatch, OptionValueFormat.Empty },
            { Option.UriPort, OptionValueFormat.UInt },
            { Option.LocationPath, OptionValueFormat.String },
            { Option.UriPath, OptionValueFormat.String },
            { Option.ContentFormat, OptionValueFormat.UInt },
            { Option.MaxAge, OptionValueFormat.UInt },
            { Option.UriQuery, OptionValueFormat.String },
            { Option.Accept, OptionValueFormat.UInt },
            { Option.LocationQuery, OptionValueFormat.String },
            { Option.ProxyUri, OptionValueFormat.String },
            { Option.ProxyScheme, OptionValueFormat.String },
            { Option.Size1, OptionValueFormat.UInt }
        };
    }
}
