using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.DTO;

namespace TodoList.ViewModels.Models
{
    internal class ItemStatusVMM: ViewModelBase
    {
        private readonly ItemStatusDTO _itemStatus;

        public int Id => _itemStatus.Id;

        public string Name => _itemStatus.Name;

        public ItemStatusVMM(ItemStatusDTO itemStatus)
        {
            _itemStatus = itemStatus;
        }

        public override bool Equals(object obj)
        {
            if (obj is ItemStatusVMM)
            {
                ItemStatusVMM itSt = (ItemStatusVMM)obj;

                return itSt.Id == Id && itSt.Name == Name;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            HashCode hc = new HashCode();

            hc.Add(Id);
            hc.Add(Name);

            return hc.ToHashCode();
        }
    }
}
