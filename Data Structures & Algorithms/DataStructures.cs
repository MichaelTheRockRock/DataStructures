using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure.Attributes
{
    public enum DataStructures {
        [Description("Linked List")]
        LinkedList,
        [Description("Hash Tables")]
        HashTable,
        [Description("Array List")]
        ArrayList,
        [Description("StringBuilder")]
        StringBuilder,
        [Description("Stack")]
        Stack,
        [Description("Queue")]
        Queue,
        [Description("Tree")]
        Tree,
        [Description("Graph")]
        Graph
    }

    public static class DataStructureExtensions
    {
        public static string GetDescription(this DataStructures value)
        {
            Type type = value.GetType();
            string? name = DataStructures.GetName(type, value);

            if (name != null)
            {
                FieldInfo? field = type.GetField(name);

                if (field != null)
                {
                    DescriptionAttribute? attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }

            return null;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class DataStructureAttribute : Attribute
    {
        private readonly DataStructures _dataStructure;

        public DataStructureAttribute(DataStructures dataStructure)
        {
            _dataStructure = dataStructure;
        }

        public DataStructures DataStructure
        {
            get { return _dataStructure; }
        }
    }
}
