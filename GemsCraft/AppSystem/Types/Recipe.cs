using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsCraft.AppSystem.Types
{
    public sealed class Recipe
    {
        public Identifier ID { get; set; }
        public string Type { get; set; }
        public object[] Data { get; set; }
    }

    public class RecipeArray: IEnumerable<Recipe>
    {
        private readonly List<Recipe> _items = new List<Recipe>();

        public void Add(Recipe r)
        {
            if (r == null) throw new ArgumentNullException(nameof(r));
            foreach (Recipe f in _items)
            {
                if (f.ID == r.ID) throw new Exception(nameof(f));
            }

            _items.Add(r);
        }

        public void Remove(Recipe r)
        {
            _items.Remove(r);
        }

        public void RemoveAt(int index)
        {
            _items.RemoveAt(index);
        }

        protected Recipe this[int i]
        {
            get => _items[i];
            set => _items[i] = value;
        }

        public void Delete(Recipe recipe)
        {
            _items.Remove(recipe);
        }

        public IEnumerator<Recipe> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
