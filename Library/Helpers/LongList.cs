using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace Library.Helpers
{
    public class LongList : List<long>, IEnumerable<SqlDataRecord>
	{
		IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
		{
			var sqlRow = new SqlDataRecord(
				new SqlMetaData("LongValue", SqlDbType.BigInt)
				);
			foreach (int id in this)
			{
				sqlRow.SetInt64(0, id);
				yield return sqlRow;
			}
		}
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
    
}
