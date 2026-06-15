namespace BoscoAFH.Common
{
    public class EnumCommand
    {
        public enum DataType
        {
            BigInt,
            Boolean,
            Byte,
            Char,
            Date,
            DateTime,
            smalldatetime,
            Decimal,
            Double,
            Money,
            Int,
            Int16,
            Int32,
            Int64,
            SByte,
            Single,
            String,
            TimeSpan,
            UInt16,
            UInt32,
            UInt64,
            ByteArray,
            Varchar,
            nVarchar,
            None,
            Memo,
            Blob,
            Text,
            Xml,
            bit,
            ntext
        }

        public enum DefaultValues
        {
            Zero = 0,
            ONE = 1,
            TWO = 2,
            THREE = 3,
            FOUR = 4,
            FIVE = 5,
            SIX = 6,
            SEVEN = 7,
            EIGHT = 8,
            NINE = 9,
            TEN = 10
        }
        public enum SQLType
        {
            SQLStatic,
            SQLDynamic,
            SQLStoredProcedure
        }

        #region DB Related

        public enum DBConnectionType
        {
            MasterDBConnection,
            ClusterDBConnection
        }

        #endregion DB Related

        public enum DataSource
        {
            DataSet,
            DataReader,
            DataTable,
            DataView,
            Scalar,
            ExecuteXmlReader
        }

        public enum RenderActionQuery
        {
            AllRecords = 0,
            SaveRecords = 1,
            DeleteRecords = 2,
            SelectById = 3,
        }

         
        public enum DefalutVlaue
        {
            defalut = 0,
            FlagId1 = 1,
            FlagId2 = 2
        }
         
    }
}
