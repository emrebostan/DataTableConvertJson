using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
public static class DataTableConvertJson
{
    public static string Convert(DataTable R, params string[] hideColums)
    {
        string _djson = "";
        int rCols = 0;
        int _drcount = R.Rows.Count;
        int _clcount = R.Columns.Count;
        string passiveColumns = "";
        foreach (string c in hideColums)
        {
            int index = R.Columns.IndexOf(c);
            if (index != -1)
            {
                passiveColumns += ":" + R.Columns[index].ColumnName + ";";
                rCols++;
            }
        }
        for (int _i = 0; _i < R.Rows.Count; _i++)
        {
            DataRow dr = R.Rows[_i];
            _djson += "{";
            for (int _x = 0; _x < R.Columns.Count; _x++)
            {
                DataColumn cl = R.Columns[_x];
                string colName = cl.ColumnName;
                if (passiveColumns.IndexOf(":" + colName + ";") == -1)
                {
                    string colType = cl.DataType.Name.ToString();
                    string colValue = dr[_x].ToString();
                    if (colValue == "")
                        colValue = "null";
                    if (colType.IndexOf("Int") != -1 || colType == "Double" || colType == "Decimal" || colType == "Byte" || colType == "SByte")
                        colType = "integer";
                    else if (colType.IndexOf("Bool") != -1)
                        colType = "bool";
                    if (colType == "integer" || colType == "bool")
                        _djson += "\"" + colName + "\":" + colValue.ToLower();
                    else
                        _djson += "\"" + colName + "\":\"" + HttpUtility.JavaScriptStringEncode(colValue) + "\"";
                    if (_x != _clcount - 1)
                        _djson += ",";
                }

            }
            _djson += "}";
            if (_i != _drcount - 1)
                _djson += ",";
        }
        return _djson;
    }
}