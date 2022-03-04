using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace Library.Helpers
{
    public class IntList : List<int>, IEnumerable<SqlDataRecord>
	{
		IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
		{
			var sqlRow = new SqlDataRecord(
				new SqlMetaData("IntValue", SqlDbType.Int)
				);
			foreach (int id in this)
			{
				sqlRow.SetInt32(0, id);
				yield return sqlRow;
			}
		}
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
    
}
