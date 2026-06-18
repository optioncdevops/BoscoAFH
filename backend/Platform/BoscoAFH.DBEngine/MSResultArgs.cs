/***********************************************************************************************************
 * Created by       : Justine
 * Created On       : 14 Mar 2018
 *
 * Reviewed By      :
 * Reviewed On      :
 *
 * Purpose          : To retrun the values from database and save the details in this  ResultArgs object
 ***********************************************************************************************************/

namespace BoscoAFH.DBEngine
{
    public class MSResultArgs
    {
        public object? dataSource = null;
        private Dictionary<string, object>? rowUniqueIdCollection = null;
        private ResultSource? resultSource = new();

        public MSResultArgs()
        {
        }

        public MSResultArgs(bool isShowExceptionMessage)
        {
            this.IsShowExceptionMessage = isShowExceptionMessage;
        }

        public bool Success { get; set; } = false;

        public bool IsShowExceptionMessage { get; set; } = true;

        public int RowsAffected { get; set; } = 0;

        public bool IsDeadLock { get; set; } = false;

        public object RowUniqueId
        {
            get;
            set
            {
                this.Success = true;
                field = value;
            }
        } = "";

        public Dictionary<string, object> RowUniqueIdCollection
        {
            get
            {
                rowUniqueIdCollection ??= new Dictionary<string, object>();

                return rowUniqueIdCollection;
            }
            set
            {
                this.Success = true;
                rowUniqueIdCollection = value;
            }
        }

        public object? ReturnValue { get; set; } = null;

        public ResultSource DataSource
        {
            get => resultSource;

            set
            {
                this.Success = true;
                resultSource.Data = value;
            }
        }

        #region Class Result Source

        public class ResultSource: IDisposable
        {
            private object? dataSource = null;

            public ResultSource()
            {
                ScalarType1 = new ScalarType();
            }

            public object Data
            {
                get => dataSource;
                set
                {
                    dataSource = value;
                    ScalarType1.SclarSource = dataSource;
                }
            }

            /// <summary>
            /// Get Dataset Object
            /// </summary>
            public DataSet TableSet => dataSource as DataSet;

            /// <summary>
            /// Get Data Table Object
            /// </summary>
            public DataTable Table => dataSource as DataTable;

            /// <summary>
            /// Get Data View Object
            /// </summary>
            public DataView TableView => dataSource as DataView;

            /// <summary>
            /// Get Sclar value
            /// </summary>
            ///
            public ScalarType Scalar => ScalarType1;

            public ScalarType? ScalarType1 { get => this.ScalarType2; set => this.ScalarType2 = value; }
            public ScalarType? ScalarType2 { get; set; } = null;

            public class ScalarType
            {
                private object? dataSource = null;

                public object SclarSource
                {
                    set => dataSource = value;
                }

                public new string ToString
                {
                    get
                    {
                        string sclarVal = "";

                        if (dataSource != null)
                        {
                            sclarVal = dataSource.ToString();
                        }

                        return sclarVal;
                    }
                }

                public int ToInteger
                {
                    get
                    {
                        int.TryParse(this.ToString, out var sclarVal);
                        return sclarVal;
                    }
                }
            }

            #region IDisposable Members

            public void Dispose()
            {
                if (ScalarType1 != null)
                {
                    ScalarType1 = null;
                }
                if (dataSource != null)
                {
                    dataSource = null;
                }
            }

            #endregion IDisposable Members
        }

        #endregion Class Result Source

        #region IDisposable Members

        public void Dispose()
        {
            resultSource?.Dispose();
            resultSource = null;
            rowUniqueIdCollection?.Clear();
            rowUniqueIdCollection = null;
        }

        #endregion IDisposable Members
    }
}
