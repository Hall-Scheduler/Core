namespace HallScheduler.Data.Common
{
    using System;
    using System.Collections.Generic;
    using Contracts;

    public class ScheduleTree<T>
        where T : IEvent
    {
        public ScheduleNode<T> Root { get; set; }

        public List<ScheduleNode<T>> TreeAsList { get; set; }

        public bool Insert(ScheduleNode<T> nodeToInsert)
        {
            return this.Insert(this.Root, nodeToInsert);
        }

        public bool Insert(ScheduleNode<T> currentNode, ScheduleNode<T> nodeToInsert)
        {
            if (currentNode == null)
            {
                this.Root = nodeToInsert;
                return true;
            }
            if (nodeToInsert == null)
            {
                return false;
            }

            if (nodeToInsert.Data.EndsAt <= currentNode.Data.StartsAt)
            {
                if (currentNode.LeftChild == null)
                {
                    // Directly insert as left child
                    currentNode.LeftChild = nodeToInsert;
                    return true;
                }
                else
                {
                    // Go left
                    return this.Insert(currentNode.LeftChild, nodeToInsert);
                }
            }
            else if (currentNode.Data.EndsAt <= nodeToInsert.Data.StartsAt)
            {
                if (currentNode.RightChild == null)
                {
                    // Directly insert  as right child
                    currentNode.RightChild = nodeToInsert;
                    return true;
                }
                else
                {
                    // Go right
                    return this.Insert(currentNode.RightChild, nodeToInsert);
                }
            }
            else
            {
                return false;
            }
        }

        public bool CanInsert(ScheduleNode<T> nodeToInsert)
        {
            return this.CanInsert(this.Root, nodeToInsert);
        }

        public bool CanInsert(ScheduleNode<T> currentNode, ScheduleNode<T> nodeToInsert)
        {
            if (currentNode == null)
            {
                return true;
            }
            if (nodeToInsert == null)
            {
                return false;
            }

            if (nodeToInsert.Data.EndsAt <= currentNode.Data.StartsAt)
            {
                if (currentNode.LeftChild == null)
                {
                    return true;
                }
                else
                {
                    // Try left
                    return this.Insert(currentNode.LeftChild, nodeToInsert);
                }
            }
            else if (currentNode.Data.EndsAt <= nodeToInsert.Data.StartsAt)
            {
                if (currentNode.RightChild == null)
                {
                    return true;
                }
                else
                {
                    // Try right
                    return this.Insert(currentNode.RightChild, nodeToInsert);
                }
            }
            else
            {
                return false;
            }
        }

        public void Print(ScheduleNode<T> startNode, int level = 1)
        {
            if (startNode != null)
            {
                Console.WriteLine($"Level: {level} - {startNode.Data.StartsAt} - {startNode.Data.EndsAt}");
            }
            else
            {
                Console.WriteLine("No elements in the tree");
            }

            if (startNode.LeftChild != null)
            {
                this.Print(startNode.LeftChild, level + 1);
            }

            if (startNode.RightChild != null)
            {
                this.Print(startNode.RightChild, level + 1);
            }
        }

        public void BuildListFromTree()
        {
            this.TreeAsList = new List<ScheduleNode<T>>();
            this.BuildListFromTree(this.Root);
        }

        public void BuildListFromTree(ScheduleNode<T> startNode)
        {
            if (startNode != null)
            {
                this.TreeAsList.Add(startNode);
            }
            else
            {
                return;
            }

            if (startNode.LeftChild != null)
            {
                this.BuildListFromTree(startNode.LeftChild);
            }

            if (startNode.RightChild != null)
            {
                this.BuildListFromTree(startNode.RightChild);
            }
        }

        public void BuildTreeFromList(List<ScheduleNode<T>> schedule)
        {
            if(schedule != null)
            {
                for (int i = 0; i < schedule.Count; i++)
                {
                    this.Insert(schedule[i]);
                }
            }
        }
    }
}
