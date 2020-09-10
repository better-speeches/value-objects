using System.Linq;
using Varyence.ValueObjects.DataAccess.Entities.Base;

namespace Varyence.ValueObjects.DataAccess.Entities
{
    public class Suffix : Entity
    {
        public static Suffix Jr = new Suffix(1, nameof(Jr));
        public static Suffix Sr = new Suffix(2, nameof(Sr));

        public static readonly Suffix[] AllSuffixes = {Jr, Sr};
        
        protected Suffix()
        {
        }

        private Suffix(int id, string name) : base(id) =>
            Name = name;
        
        public string Name { get; }

        public static Suffix FromId(int id) => 
            AllSuffixes.SingleOrDefault(s => s.Id == id);
    }
}