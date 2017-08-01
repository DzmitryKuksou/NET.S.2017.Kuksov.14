using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Matrixs
{
    public class ElementEventArgs 
    {
        #region properties
        public int Row { get; }
        public int Column { get; }
        #endregion

        #region c-tor
        public ElementEventArgs(int Row, int Column)
        {
            this.Row = Row;
            this.Column = Column;
        }
        #endregion
    }

}
