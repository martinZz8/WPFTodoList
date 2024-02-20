using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Models
{
    [Table("ItemStatus")]
    internal class ItemStatus
    {
        [Key]
        public int Id { get; set; } //old type: Guid (generated in sql by NEWID() func)

        public string Name { get; set; }

        // From: https://www.entityframeworktutorial.net/code-first/inverseproperty-dataannotations-attribute-in-code-first.aspx
        [InverseProperty(nameof(TodoItem.Status))]
        public ICollection<TodoItem> TodoItems { get; set; }
    }
}
