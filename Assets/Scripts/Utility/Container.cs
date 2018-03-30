using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Container : MonoBehaviour
{
    [Serializable]
    public class ContainerItem
    {
        public Guid Id; // Generate Unique ID
        public string Name;
        public int Maximum;

        private int amountTaken;

        public ContainerItem()
        {
            Id = Guid.NewGuid();
        }

        public int Remaining
        {
            get
            {
                return Maximum - amountTaken; // Remaining amount left in the container
            }
        }

        public int GetAmount(int value)
        {
            // if the amount that we have + that we have is too greater than the maximum
            if ((amountTaken + value) > Maximum)
            {
                int tooMuch = (amountTaken + value) - Maximum;
                amountTaken = Maximum;
                return value - tooMuch;
            }
            amountTaken += value;
            return value;
        }
    }

    public List<ContainerItem> items = new List<ContainerItem>();

    public Guid Add(string name, int maximum)
    {
        items.Add(new ContainerItem
        {
            Id = Guid.NewGuid(),
            Maximum = maximum,
            Name = name
        });

        return items.Last().Id;
    }

    public int TakeFromContainer(Guid id, int amount)
    {
        ContainerItem containerItem = items.Where(x => x.Id == id).FirstOrDefault();
        if (containerItem == null)
        {
            return -1;
        }
        return containerItem.GetAmount(amount);
    }


}