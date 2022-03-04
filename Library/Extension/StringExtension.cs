using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Extension
{
    public static class StringExtension
    {
        public static string SubStringAfterIndex(this string source, int tail_length)
        {
            if (tail_length >= source.Length)
                return source;
            return source.Substring(source.Length - tail_length);
        }

        public static string SubStringAfterString(string value, string a)
        {
            int posA = value.LastIndexOf(a);
            if (posA == -1)
            {
                return "";
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= value.Length)
            {
                return "";
            }
            return value.Substring(adjustedPosA);
        }

        public static string SubStringBeforeIndex(this string source, int tail_length)
        {
            if (tail_length >= source.Length)
                return source;
            return source.Substring(0, tail_length);
        }

        public static string SubStringBeforeString(string value, string a)
        {
            int posA = value.IndexOf(a);
            if (posA == -1)
            {
                return "";
            }
            return value.Substring(0, posA);
        }

        public static string SubStringBetweenIndex(this string source, int start, int tail_length)
        {
            if (tail_length >= source.Length - start)
                return source.Substring(start, source.Length - start);
            return source.Substring(start, tail_length);
        }

        public static string SubStringBetweenStrings(string value, string a, string b)
        {
            int posA = value.IndexOf(a);
            int posB = value.LastIndexOf(b);
            if (posA == -1)
            {
                return "";
            }
            if (posB == -1)
            {
                return "";
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= posB)
            {
                return "";
            }
            return value.Substring(adjustedPosA, posB - adjustedPosA);
        }

        public static string RemoveLastComma(string str)
        {
            int index = str.LastIndexOf(",");
            return str.Substring(0, index);
        }

        public static string ConvertOrderNumberToStringLength5(int num) // length  = 5
        {
            string str = num.ToString();
            switch (str.Length)
            {
                case 1:
                    str = string.Concat("0000", str);
                    break;
                case 2:
                    str = string.Concat("000", str);
                    break;
                case 3:
                    str = string.Concat("00", str);
                    break;
                case 4:
                    str = string.Concat("0", str);
                    break;
                default:
                    break;
            }
            return str;
        }

        public static string ConvertOrderNumberToStringLength6(int num) // length  = 5
        {
            string str = num.ToString();
            switch (str.Length)
            {
                case 1:
                    str = string.Concat("00000", str);
                    break;
                case 2:
                    str = string.Concat("0000", str);
                    break;
                case 3:
                    str = string.Concat("000", str);
                    break;
                case 4:
                    str = string.Concat("00", str);
                    break;
                case 5:
                    str = string.Concat("0", str);
                    break;
                default:
                    break;
            }
            return str;
        }

        public static string ConvertOrderNumberToStringLength4(int num) // length  = 4
        {
            string str = num.ToString();
            switch (str.Length)
            {
                case 1:
                    str = string.Concat("000", str);
                    break;
                case 2:
                    str = string.Concat("00", str);
                    break;
                case 3:
                    str = string.Concat("0", str);
                    break;
                default:
                    break;
            }
            return str;
        }

        public static string ConvertOrderNumberToStringLength3(int num) // length  = 3
        {
            string str = num.ToString();
            switch (str.Length)
            {
                case 1:
                    str = string.Concat("00", str);
                    break;
                case 2:
                    str = string.Concat("0", str);
                    break;
                default:
                    break;
            }
            return str;
        }

        public static string ConvertOrderNumberToStringLength2(int num) // length  = 2
        {
            string str = num.ToString();
            switch (str.Length)
            {
                case 1:
                    return string.Concat("0", str);
            }
            return str;
        }

        public static double IntToDouble(int? e)
        {
            if (e == null)
            {
                return 0;
            }

            string a = e.ToString();
            return Convert.ToDouble(a);
        }

        public static string FormatDateTimeToStringDate(DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return "";
            }
            var ddd = dateTime.ToString();
            var dddd = Convert.ToDateTime(ddd);
            return dddd.ToString("yyyy-MM-dd");
        }

        public static string FormatNumberStringInt(int? intNumber)
        {
            if (intNumber == null)
            {
                return "0";
            }

            return string.Format("{0:#,0}", intNumber);
        }

        public static int FormatNumberInt(int? intNumber)
        {
            if (intNumber == null)
            {
                return 0;
            }
            var ddd = intNumber.ToString();
            var dddd = Convert.ToInt32(ddd);
            return dddd;
        }

        public static string FormatNumberStringInt(double? doubleNumber)
        {
            if (doubleNumber == null)
            {
                return "0";
            }

            return string.Format("{0:#,0.00}", doubleNumber);
        }

        public static string FormatNumberStringFloat(float? floatNumber)
        {
            if (floatNumber == null)
            {
                return "0";
            }

            return string.Format("{0:#,0.00}", floatNumber);
        }

        public static string FormatNumberStringDecimal(decimal? decimalNumber)
        {
            if (decimalNumber == null)
            {
                return "0";
            }

            return string.Format("{0:#,0.00}", decimalNumber);
        }

        public static string FormatNumberStringDecimalToInt(decimal? decimalNumber)
        {
            if (decimalNumber == null)
            {
                return "0";
            }

            return string.Format("{0:#,0}", decimalNumber);
        }

        public static string FormatDateTimeToStringDateTime(DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return "";
            }
            var ddd = dateTime.ToString();
            var dddd = Convert.ToDateTime(ddd);
            return dddd.ToString("yyyy-MM-dd HH:mm");
        }

        public static string FormatTimeSpanToString(TimeSpan? timeSpan)
        {
            if (timeSpan == null)
            {
                return "";
            }
            var ddd = timeSpan.ToString();
            var dddd = Convert.ToDateTime(ddd);
            return dddd.ToString("HH:mm");
        }

        public static string catchuoi(string chuoi, int gioi_han)
        {
            // nếu độ dài chuỗi nhỏ hơn hay bằng vị trí cắt
            // thì không thay đổi chuỗi ban đầu
            if (chuoi.Length <= gioi_han)
            {

                return chuoi;
            }
            else
            {
                /*
                  so sánh vị trí cắt
                  với kí tự khoảng trắng đầu tiên trong chuỗi ban đầu tính từ vị trí cắt
                  nếu vị trí khoảng trắng lớn hơn
                  thì cắt chuỗi tại vị trí khoảng trắng đó
                  */
                if (chuoi.IndexOf(" ", gioi_han) > gioi_han)
                {
                    var new_gioihan = chuoi.IndexOf(" ", gioi_han);
                    var new_chuoi = chuoi.Substring(0, new_gioihan) + "...";
                    return new_chuoi;
                } // trường hợp còn lại không ảnh hưởng tới kết quả
                var new_chuoi1 = chuoi.Substring(0, gioi_han) + "...";
                return new_chuoi1;
            }
        }

        public static string FormatId(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return "";
            }

            string start = id.Substring(0, 2);
            string end = id.Substring(2);

            string zero = "";

            for (int i = 0; i < 11 - id.Length; i++)
            {
                zero += "0";
            }
            string newId = start + zero + end;
            return newId.ToUpper();
        }

        public static string FormatIdZero(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return "";
            }

            string start = id.Substring(0, 2);
            string end = id.Substring(2);
            int endNew = Int32.Parse(end);
            string newId = start + endNew;
            return newId.ToUpper();
        }
    }
}
