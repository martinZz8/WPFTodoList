using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Models
{
    [Table("TodoItem")]
    internal class TodoItem
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        // From: https://www.entityframeworktutorial.net/code-first/inverseproperty-dataannotations-attribute-in-code-first.aspx
        [ForeignKey(nameof(ItemStatusID))]
        public ItemStatus Status { get; set; }

        public int ItemStatusID { get; set; }


        //[DefaultValue("DateTime.UtcNow")]
        //Auto creation of date when insertion is preformed (from SQL): [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //Auto creation of date when add and update is preformed: [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; } //GETDATE() function in sql

        //public DateTime DateCreated
        //{
        //    get
        //    {
        //        return this.dateCreated.HasValue
        //           ? this.dateCreated.Value
        //           : DateTime.UtcNow;
        //    }

        //    set { this.dateCreated = value; }
        //}

        //private DateTime? dateCreated = null;
    }
}
