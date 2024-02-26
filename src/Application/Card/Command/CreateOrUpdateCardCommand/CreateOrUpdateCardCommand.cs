using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Card.Command.CreateOrUpdateCardCommand
{
    public class CreateOrUpdateCardCommand : IRequest<Unit>
    {
        //Tạo các field để hứng ở đây
        public string field1 { get; set; }
        public int field2 { get;set; }
        public string field3 { get; set; }
    }
}
