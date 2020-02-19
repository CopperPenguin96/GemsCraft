using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using java.lang;

namespace GemsCraft.Configuration
{
    public class ConfigDescriptorAttribute: Attribute
    {
        public string Description { get; set; }

        public ConfigDescriptorAttribute()
        {
            // Ignored
        }

        public ConfigDescriptorAttribute(string description)
        {
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        public virtual bool Validate(object o, out string error)
        {
            error = "";
            return true;
        }
    }

    public sealed class IntDescriptorAttribute : ConfigDescriptorAttribute
    {
        public int MinValue { get; set; }

        public int MaxValue { get; set; }

        public IntDescriptorAttribute(string description)
            : base(description)
        {

        }

        public IntDescriptorAttribute(int minValue, int maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public override bool Validate(object o, out string error)
        {
            if (o is int i)
            {
                if (i < MinValue)
                {
                    error = $"Number is too small. Expecting above {MinValue}.";
                    return false;
                }
                else
                {
                    error = $"Number is too big. Expecting below {MaxValue}";
                    return false;
                }

                return true;
            }
            else
            {
                error = "Object is not int";
                return false;
            }
        }
    }
    public sealed class StringDescriptorAttribute : ConfigDescriptorAttribute
    {
        public int MinLength { get; set; } = -1;
        public int MaxLength { get; set; } = -1;

        public StringDescriptorAttribute(string description)
            : base(description)
        {

        }

        public StringDescriptorAttribute(string description, int min, int max)
            : base(description)
        {
            MinLength = min;
            MaxLength = max;
        }

        public override bool Validate(object o, out string error)
        {
            if (o is string str)
            {
                if (str.Length < MinLength && MinLength > 0)
                {
                    error = $"Expected min. length of {MinLength} " +
                            $"but got {str.Length}.";
                    return false;
                }

                if (str.Length > MaxLength && MaxLength > 0)
                {
                    error = $"Expected max. length of {MaxLength} " +
                            $"but got {str.Length}.";
                    return false;
                }

                error = "";
                return true;
            }
            else
            {
                error = "Type is not string.";
                return false;
            }
        }
    }
}
