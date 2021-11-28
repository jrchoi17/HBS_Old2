using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBS_Shared
{
    public class CException
    {
        public enum Type { Null = 0, NotNumber, UnsupportedKeyword, NoFile, InvalidRange }

        public const string NotNumber = "The value is NOT a number.";
        public const string NotPositiveNumber = "The value is NOT a positive number.";
        public const string UnsupportedKeyword = "Unsupported Keyword...";
        public const string NoFile = "There is no file.";
        public const string InvalidColumnRow = "The number of column/row does not match the table.";
        public const string InvalidRange = "The value has invalid range.";
        
            public static System.Exception Show(Type type = Type.Null)
        {
            switch (type)
            {
                case Type.Null:
                    return new System.Exception((new StackTrace()).GetFrame(1).GetMethod().Name + "()");
                case Type.NotNumber:
                    return new System.Exception((new StackTrace()).GetFrame(1).GetMethod().Name + "() " + NotNumber);
                case Type.UnsupportedKeyword:
                    return new System.Exception((new StackTrace()).GetFrame(1).GetMethod().Name + "() " + UnsupportedKeyword);
                case Type.NoFile:
                    return new System.Exception((new StackTrace()).GetFrame(1).GetMethod().Name + "() " + NoFile);
                case Type.InvalidRange:
                    return new System.Exception((new StackTrace().GetFrame(1).GetMethod().Name + "()" + InvalidRange));
                default:
                    return new System.Exception((new StackTrace()).GetFrame(0).GetMethod().Name);
            }
        }
    }
}
