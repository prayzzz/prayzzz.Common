using System;
using System.ComponentModel.DataAnnotations;

namespace prayzzz.Common.Dbo
{
    public abstract class BaseDbo
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.MinValue;

        public DateTime LastModified { get; set; } = DateTime.MaxValue;
    }
}