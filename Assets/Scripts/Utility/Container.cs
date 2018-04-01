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
        public int amountTaken;

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

        public void SetAmount(int amount)
        {
            amountTaken -= amount;
            if (amountTaken < 0)
            {
                amountTaken = 0;
            }
        }
    }

    public List<ContainerItem> items;
    public delegate void OnContainerReady();
    public event OnContainerReady onContainerReady;

    private void Awake()
    {
        items = new List<ContainerItem>();
        if (onContainerReady != null)
        {
            onContainerReady();
        }
    }

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

    public void Put(string name, int amount)
    {
        // get continerItem by its name
        ContainerItem containerItem = items.Where(x => x.Name == name).FirstOrDefault();
        if (containerItem == null)
        {
            return;
        }
        containerItem.SetAmount(amount);
    }

    public int TakeFromContainer(Guid id, int amount)
    {
        ContainerItem containerItem = GetContainerItem(id);
        if (containerItem == null)
        {
            return -1;
        }
        return containerItem.GetAmount(amount);
    }

    public int GetAmountRemaining(Guid id)
    {
        ContainerItem containerItem = GetContainerItem(id);
        if (containerItem == null)
        {
            return -1;
        }
        return containerItem.Remaining;
    }

    private ContainerItem GetContainerItem(Guid id)
    {
        // get continerItem by its ID
        ContainerItem containerItem = items.Where(x => x.Id == id).FirstOrDefault();
        return containerItem;
    }
}