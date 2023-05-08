#if UNITY_EDITOR
namespace LabraxEditor
{
    using System;
    using System.Linq;
    using UnityEngine;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class MetaDirector
    {
        public static object CreateMeta<T>(T meta, MenuType menuType) where T : ScriptableObjectExtended
        {
            object result = null;

            ScriptableObjectCreator.ShowDialog<T>(meta, PathsHolder.GetPath(meta, menuType), obj =>
            {
                if (obj.name != "")
                    obj.FileName = obj.name;
                else
                    obj.FileName = "NewMeta";
                obj.SaveObject();
                result = obj;
            }, false);
            
            return result;
        }

        public static List<MetaTab> GetMetaTabs(MenuType menuType)
        {
            List<MetaTab> result = new List<MetaTab>();

            var test = ReflectiveEnumerator.GetEnumerableOfType<ScriptableObjectExtended>();
            switch (menuType)
            {
                case MenuType.Meta:
                    test = ReflectiveEnumerator.GetEnumerableOfType<ScriptableMeta>();
                    break;
                case MenuType.Settings:
                    test = ReflectiveEnumerator.GetEnumerableOfType<ScriptableSettings>();
                    break;
            }
            foreach (var t in test)
            {
                string tabName = Regex.Replace(t.Name, "([a-z])([A-Z])", "$1 $2");//string.Format(" | {0} | ", t.Name);
                result.Add(new MetaTab(tabName, t));
            }
            result.Sort();

            return result;
        }
    }

    public class MetaTab : IComparable<MetaTab>
    {
        public string TabName { get; private set; }
        public ScriptableObjectExtended SuperType => (ScriptableObjectExtended)ScriptableObject.CreateInstance(SOType);
        public Type SOType { get; private set; }

        public MetaTab(string name, ScriptableObjectExtended superType)
        {
            TabName = name;
        }

        public MetaTab(string name, Type superType)
        {
            TabName = name;
            SOType = superType;
        }

        public int CompareTo(MetaTab other)
        {
            return TabName.CompareTo(other.TabName);
        }
    }

    public static class ReflectiveEnumerator
    {
        static ReflectiveEnumerator() { }

        public static IEnumerable<Type> GetEnumerableOfType<T>(params object[] constructorArgs) where T : class, IComparable<T>
        {
            var objects = System.Reflection.Assembly.GetAssembly(typeof(T)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T)));

            return objects;
        }
    }
}
#endif