/***********************************************************************************************************
* Created by       : Justine
* Created On       : 14 Mar 2018
*
*
* Reviewed By      :
* Reviewed On      :
*
* Purpose          : This is to handle parameters passing from one layer to other layers
***********************************************************************************************************/

namespace BoscoAFH.DBEngine
{
    public class DataValueBase(string fieldName, string fieldValue, DBEnumCommand.DataType dataType)
    {
        public string FieldName { set; get; } = fieldName;

        public string FieldValue { set; get; } = fieldValue;

        public DBEnumCommand.DataType FieldDataType { set; get; } = dataType;
    }

    /// <summary>
    /// Store the Values in the List object
    /// </summary>
    public class DataValue
    {
        private List<DataValueBase> DataItem = new();

        public int Count => DataItem.Count;

        public DataValueBase this[int index]
        { get => DataItem[index];
        }

        public void Add(string fieldName, string fieldValue, DBEnumCommand.DataType dataType)
        {
            DataItem.Add(new DataValueBase(fieldName, fieldValue, dataType));
        }

        public void Add(string fieldName, object fieldValue, DBEnumCommand.DataType dataType = DBEnumCommand.DataType.Varchar)
        {
            Add(fieldName, fieldValue.ToString(), dataType);
        }

        public DataValueBase GetItem(int index)
        {
            return DataItem[index];
        }

        // Added by Justine on 23-Nov-2017 - Get Index by Field name
        public DataValueBase GetItem(string byKey)
        {
            int index = 0;
            try
            {
                index = DataItem.FindIndex(a => a.FieldName == byKey);
            }
            catch (Exception)
            {
                //new ErrorLog().WriteLog(ex);
            }

            return DataItem[index];
        }

        // Added by Justine on 23-Nov-2017 - Get Index by Field name
        public string GetItembyName(string byKey = "")
        {
            int index = 0;
            string FieldValue = string.Empty;
            try
            {
                index = DataItem.FindIndex(a => a.FieldName == byKey);
                FieldValue = DataItem[index].FieldValue;
            }
            catch (Exception)
            {
                //new ErrorLog().WriteLog(ex);
            }

            return FieldValue;
        }

        public void Clear()
        {
            DataItem.Clear();
        }

        public void Remove(DataValueBase dv)
        {
            DataItem.Remove(dv);
        }

        public void RemoveItem(int index)
        {
            DataItem.RemoveAt(index);
        }

        public void RemoveItem(string byKey)
        {
            int index = 0;
            try
            {
                index = DataItem.FindIndex(a => a.FieldName == byKey);
            }
            catch (Exception)
            {
                //new ErrorLog().WriteLog(ex);
            }
            DataItem.RemoveAt(index);
        }

        public IEnumerator GetEnumerator()
        {
            return DataItem.GetEnumerator();
        }
    }
}
