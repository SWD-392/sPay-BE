using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.BO.DTOs.Admin.Transaction.Response
{
    public class TransactionResponse
    {
        public string TransactionKey { get; set; } = null!;
        public string FromWalletKey { get; set; } = null!;
		public string FromToWalletKey { get; set; } = null!;

	}
}
