// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("FYJuZ3Nhka76cv5W0U2ot8SXV93SMDKtLNoFL6GBQJPLzMe9CVK9vsXU99r5xsMtHFJ7E3ToMtHv9+HIR4u6EFInPFDpaZX1Zeo0+Ffgo6eMOaVQlBmwBN2sJD6YhY5NJjp7jS5gIkO8yxnFDFI+1GWMatmDNdgjJ5UWNScaER49kV+R4BoWFhYSFxSVFhgXJ5UWHRWVFhYXg+A2rqAKHfeqpjuQZY8LeabJ5ZN34W0xxaljn0E2HKjM6w7P8+05eBXfinNrAQkTOXZ5e+x+TRYikDJyHtegXtvzJ0tzaKSg24/BkedIXAxqlzLSAjtje6cMhpd64JIkk5PF45wX89ec0nXLYrfRmxEFMBmz4j5jvpP84VDgtIE5CuM2z97nrhUUFhcW");
        private static int[] order = new int[] { 11,13,12,9,4,7,11,13,8,12,11,12,12,13,14 };
        private static int key = 23;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
