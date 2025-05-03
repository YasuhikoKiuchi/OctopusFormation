using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctopusFormation
{
internal class Octopus
{
    public Point P { get; set; }

    public Point V { get; set; }

    public Octopus(Point p)
    {
        P = p;
        V = new Point(0, 0);
    }
}
}
