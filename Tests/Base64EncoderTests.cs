using NUnit.Framework;
using Rossoforge.Utils.Encoding;
using System;

namespace Rossoforge.Utils.Tests
{
    public class Base64EncoderTests
    {
        [Test]
        public void EncodeDecode_RoundTrip_Works()
        {
            string input = "Hello Base64!";
            string encoded = Base64Encoder.Encode(input);

            bool success = Base64Encoder.TryDecode(encoded, out string decoded);
            Assert.IsTrue(success);
            Assert.AreEqual(input, decoded);
        }

        [Test]
        public void TryDecode_InvalidInput_ReturnsFalse()
        {
            bool success = Base64Encoder.TryDecode("InvalidBase64$$$", out var output);
            Assert.IsFalse(success);
            Assert.IsNull(output);
        }

        [Test]
        public void SetKey_NullOrEmpty_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => Base64Encoder.SetKey(null));
            Assert.Throws<ArgumentException>(() => Base64Encoder.SetKey(""));
        }
    }
}