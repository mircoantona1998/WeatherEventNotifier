using System;
using System.Collections.Generic;

namespace Userdata.Models
{
    public partial class MailUser
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string Mail { get; set; } = null!;
    }
}
