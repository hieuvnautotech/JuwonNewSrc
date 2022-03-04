using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace Library.Helpers
{
    public class StringList : List<string>, IEnumerable<SqlDataRecord>
    {
		IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
		{
			var sqlRow = new SqlDataRecord(
				new SqlMetaData("StringValue", SqlDbType.NVarChar)
				);
			foreach (string item in this)
			{
				sqlRow.SetString(0, item);
				yield return sqlRow;
			}
		}
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}
