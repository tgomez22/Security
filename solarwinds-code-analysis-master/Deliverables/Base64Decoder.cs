/*
*   Below are two methods that are lifted from the "OrionImprovementBusinessLayer.cs" file. 
*   These methods are used in tandem to decode base64 encoded strings. The malware author(s)
*   used a custom alphabet to encode their strings, so normal base64 decoding methods will not
*   work. In the "main" method, I placed all of the base64 encoded strings found within the malware
*   into an array of strings. This array is iterated through and the strings are printed out in their
*   plain text form.
*/



using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        public static string Unzip(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            try
            {
                return Encoding.UTF8.GetString(Decompress(Convert.FromBase64String(input)));
            }
            catch (Exception ex)
            {
                return input;
            }
        }


        public static byte[] Decompress(byte[] input)
        {
            using (MemoryStream memoryStream1 = new MemoryStream(input))
            {
                using (MemoryStream memoryStream2 = new MemoryStream())
                {
                    using (DeflateStream deflateStream = new DeflateStream((Stream)memoryStream1, CompressionMode.Decompress))
                        deflateStream.CopyTo((Stream)memoryStream2);
                    return memoryStream2.ToArray();
                }
            }
        }
        static void Main(string[] args)
        {
            string[] toDecode = {
            "C07NSU0uUdBScCvKz1UIz8wzNor3Sy0pzy/KdkxJLChJLXLOz0vLTC8tSizJzM9TKM9ILUpV8AxwzUtMyklNsS0pKk0FAA==",
            "c0ktTi7KLCjJzM8DAA==",
            "83V0dkxJKUotLgYA",
            "c/FwDnDNS0zKSU0BAA==",
            "c/FwDghOLSpLLQIA",
            "c/EL9sgvLvFLzE0FAA==",
            "c/ELdsnPTczMCy5NS8usCE5NLErO8C9KSS0CAA==",
            "c/ELDk4tKkstCk5NLErO8C9KSS0CAA==",
            "8wxwTEkpSi0uBgA=",
            "8wwILk3KSy0BAA==",
            "c0lNSyzNKfEMcE8sSS1PrAQA",
            "C07NSU0uUdBScCvKz1UIz8wzNor3L0gtSizJzEsPriwuSc0FAA==",
            "c04sKMnMzwMA",
            "88wrLknMyXFJLEkFAA==",
            "8y9KT8zLrEosyczPAwA=",
            "C0pNzywuSS1KTQktTi0CAA==",
            "C0stKs7MzwMA",
            "i3aNVag2qFWoNgRio1oA",
            "8/B2jYz38Xd29In3dXT28PRzjQn2dwsJdwxyjfHNTC7KL85PK4lxLqosKMlPL0osyKgEAA==",
            "801MzsjMS3UvzUwBAA==",
            "MzTQA0MA",
            "MzI11TMAQQA=",
            "MzQ30jM00zPQMwAA",
            "MzI11TMyMdADQgA=",
            "M7Q00jM0s9Az0DMAAA==",
            "MzI11TMCYgM9AwA=",
            "MzIy0TMAQQA=",
            "MzIx0ANDAA==",
            "S0s2MLCyAgA=",
            "S0s1MLCyAgA=",
            "S0tNNrCyAgA=",
            "S0tLNrCyAgA=",
            "S0szMLCyAgA=",
            "S0szMLCyAgA=",
            "MzHUszDRMzS11DMAAA==",
            "MzI11TOCYgMA",
            "MzfRMzQ00TMy0TMAAA==",
            "MzI11TMCYRMLPQMA",
            "MzQ10TM0tNAzNDHQMwAA",
            "MzI11TOCYgMA",
            "MzI01zM0M9Yz1zMAAA==",
            "MzI11TOCYgMA",
            "MzLQMzQx0ANCAA==",
            "MzI11TMyNdEz0DMAAA==",
            "szTTMzbUMzQ30jMAAA==",
            "MzI11TOCYgMA",
            "MzQ21DMystAzNNIzAAA=",
            "MzI11TMCYyM9AwA=",
            "MzQx0bMw0zMyMtMzAAA=",
            "MzI11TOCYgMA",
            "s9AztNAzNDHRMwAA",
            "MzI11TMCYxM9AwA=",
            "M7TQMzQ20ANCAA==",
            "MzI11TMCYgM9AwA=",
            "MzfUMzQ10jM11jMAAA==",
            "MzI11TOCYgMA",
            "s7TUM7fUM9AzAAA=",
            "MzI11TMCYgM9AwA=",
            "szDXMzK20LMw0DMAAA==",
            "MzI11TMCYRMLPQMA",
            "M7S01DMyMNQzNDTXMwAA",
            "MzI11TOCYgMA",
            "M7Qw0TM30jPQMwAA",
            "MzI11TMyNdEz0DMAAA==",
            "07DP1NSIjkvUrYqtidPUKEktLoHzVTQB",
            "07DP1NQozs9JLCrPzEsp1gQA",
            "C0otyC8qCU8sSc5ILQpKLSmqBAA=",
            "C0otyC8qCU8sSc5ILQrILy4pyM9LBQA=",
            "SyzI1CvOz0ksKs/MSynWS87PBQA=",
            "SywrLstNzskvTdFLzs8FAA==",
            "SywoKK7MS9ZNLMgEAA==",
            "Sy3VLU8tLtE1BAA=",
            "Ky3WLU8tLtE1AgA=",
            "Ky3WTU0sLtE1BAA=",
            "Ky3WTU0sLtE1AgA=",
            "M7UwTkm0NDHVNTNKTNM,1NEi10DWxNDDSTbRIMzIwTTY3SjJKBQA=",
            "8/B2jYx39nEMDnYNjg/y9w8BAA==",
            "8/B2DgIA",
            "8/B2jYx3Dg0KcvULiQ8Ndg0CAA==",
            "8/B2DgUA",
            "8/B2jYz38Xd29In3dXT28PRzBQA=",
            "8/D28QUA",
            "8/B2jYwPDXYNCgYA",
            "8/AOBQA=",
            "8/B2jYx3Dg0KcvULiXf293PzdAcA",
            "8/B2dgYA",
            "8/B2jYwPcA1y8/d19HN2jXdxDHEEAA==",
            "8/AOcAEA",
            "8/B2jYx3ifSLd3EMcQQA",
            "8/B2cQEA",
            "C9Y11DXVBQA=",
            "0zU1MAAA",
            "c0zJzczLLC4pSizJLwIA",
            "C07NSU0uUdBScCvKz1UIz8wzNooPLU4tckxOzi/NKwEA",
            "C/Z0AQA=",
            "88lPTsxxTE7OL80rAQA=",
            "KykqTQUA",
            "C04NSi0uyS9KDSjKLMvMSU1PBQA=",
            "C04NScxO9S/PSy0qzsgsCCjKLMvMSU1PBQA=",
            "C44MDnH1BQA=",
            "MwEA",
            "MwUA",
            "MwYA",
            "C07NSU0uUdBScCvKz1UIz8wzNooPriwuSc11KcosSy0CAA==",
            "C0gsyfBLzE0FAA==",
            "C44MDnH1jXEuLSpKzStxzs8rKcrPCU4tiSlOLSrLTE4tBgA=",
            "Cy5JLCoBAA==",
            "Cy5JLCoBAA==",
            "C44MDnH1jXEuLSpKzStxzs8rKcrPCU4tiSlOLSrLTE4tBgA=",
            "Cy5JLCoBAA==",
            "Cy5JLCoBAA==",
            "i6420DGtjVWoNqzlAgA=",
            "C07NSU0uUdBScCvKz1UIz8wzNooPKMpPTi0uBgA=",
            "c08t8S/PSy0CAA==",
            "i6420DGtjVWoNtTRNTSrVag2quWsNgYKKVSb1MZUm9ZyAQA=",
            "CyjKT04tLvZ0AQA=",
            "80vMTQUA",
            "C0gsSs0rCSjKT04tLvZ0AQA=",
            "c0zJzczLLC4pSizJLwIA",
            "qzaoVag2rFXwCAkJ0K82quUCAA==",
            "U4qpjjbQtUzUTdONrTY2q42pVapRgooABYxQuIZmtUoA",
            "80zT9cvPS9X1TSxJzgAA",
            "UyotTi3yTFGyUqo2qFXSAQA=",
            "UypOLS7OzM/zTFGyUqo2qFXSAQA=",
            "UyouSS0oVrKKBgA=",
            "UwrJzE0tLknMLVCyUorRd0ksSdWoNqjVjNFX0gEA",
            "U/LMS0mtULKqNqjVAQA=",
            "U3ItS80rCaksSFWyUvIvyszPU9IBAA==",
            "U3ItS80r8UvMTVWyUgKzfRPzEtNTi5R0AA==",
            "U3IpLUosyczP8y1Wsqo2qNUBAA==",
            "UwouTU5OTU1JTVGyKikqTdUBAA==",
            "U/JNLS5OTE9VslKqNqhVAgA=",
            "SywoyMlMTizJzM/TzyrOzwMA",
            "SywoyMlMTizJzM/Tz08uSS3RLS4pSk3MBQA=",
            "0y3Kzy8BAA==",
            "001OLSoBAA==",
            "0y3NyyxLLSpOzIlPTgQA",
            "001OBAA=",
            "0y0oysxNLKqMT04EAA==",
            "0y3JzE0tLknMLQAA",
            "003PyU9KzAEA",
            "0y1OTS4tSk1OBAA=",
            "K8jO1E8uytGvNqitNqytNqrVA/IA",
            "c8rPSQEA",
            "c8rPSfEsSczJTAYA",
            "c60oKUp0ys9JAQA=",
            "c60oKUp0ys9J8SxJzMlMBgA=",
            "8yxJzMlMBgA=",
            "88lMzygBAA==",
            "88lMzyjxLEnMyUwGAA==",
            "C0pNL81JLAIA",
            "C07NzXTKz0kBAA==",
            "C07NzXTKz0nxLEnMyUwGAA==",
            "yy9IzStOzCsGAA==",
            "y8svyQcA",
            "SytKTU3LzysBAA==",
            "C84vLUpOdc5PSQ0oygcA",
            "C84vLUpODU4tykwLKMoHAA==",
            "C84vLUpO9UjMC07MKwYA",
            "C84vLUpO9UjMC04tykwDAA==",
            "S8vPKynWL89PS9OvNqjVrTYEYqNa3fLUpDSgTLVxrR5IzggA",
            "C87PSSwKz8xLKQYA",
            "03POLypJrQjIKU3PzAMA",
            "0/MvyszPAwA=",
            "C88sSs1JLS4GAA==",
            "C/UEAA==",
            "C89MSU8tKQYA",
            "8wvwBQA=",
            "cyzIz8nJBwA=",
            "c87JL03xzc/LLMkvysxLBwA=",
            "88tPSS0GAA==",
            "C8vPKc1NLQYA",
            "88wrSS1KS0xOLQYA",
            "c87PLcjPS80rKQYA",
            "Ky7PLNAvLUjRBwA=",
            "06vIzQEA",
            "Ky7PLNB3LUvNKykGAA==",
            "Ky7PLNAPLcjJT0zRSyzOqAAA",
            "881MLsovzk8r0XUuqiwoyXcM8NQHAA==",
            "C87PSSwKz8xLKfYvyszP88wtKMovS81NzStxzskEkvoA",
            "i/EvyszP88wtKMovS81NzSuJCc7PSSwKz8xLKdZDl9NLrUgFAA==",
            "M9YzAEJjCyMA",
            "Kyo0Ti9OzCkxKzXMrEyryi8wNTdKMbFMyquwSC7LzU4tz8gCAA==",
            "M4jX1QMA",
            "K8gwSs1MyzfOMy0tSTfMskixNCksKkvKzTYoTswxN0sGAA==",
            "MzA0MjYxNTO3sExMSk5JTUvPyMzKzsnNyy8oLCouKS0rr6is0o3XAwA=",
            "C04NzigtSckvzwsoyizLzElNTwUA" };

            foreach(string encodedWord in toDecode)
            {
                Console.WriteLine(Unzip(encodedWord));
            }
        }
    }
}
