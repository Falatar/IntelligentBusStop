using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentBusStop
{
    public class Pair<X, Y>
    {
        private X _x;
        private Y _y;

        public Pair(X first, Y second)
        {
            _x = first;
            _y = second;
        }

        public void setX(X value)
        {
            _x = value;
        }

        public void setY(Y value)
        {
            _y = value;
        }

        public X first { get { return _x; } }

        public Y second { get { return _y; } }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj == this)
                return true;
            Pair<X, Y> other = obj as Pair<X, Y>;
            if (other == null)
                return false;

            return
                (((first == null) && (other.first == null))
                    || ((first != null) && first.Equals(other.first)))
                  &&
                (((second == null) && (other.second == null))
                    || ((second != null) && second.Equals(other.second)));
        }

        public override int GetHashCode()
        {
            int hashcode = 0;
            if (first != null)
                hashcode += first.GetHashCode();
            if (second != null)
                hashcode += second.GetHashCode();

            return hashcode;
        }
    }
}
