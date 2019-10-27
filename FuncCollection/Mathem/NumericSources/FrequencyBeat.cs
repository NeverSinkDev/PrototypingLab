using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncCollection.Mathem.NumericSources
{
    public class FrequencyBeat
    {
        private double frequency = 0;

        private double currentIter = 0;

        private double lastStep = 0;

        List<FrequencyBeat> DependentBeats = new List<FrequencyBeat>();

        public FrequencyBeat(double frequency)
        {
            this.frequency = frequency;
            this.lastStep = frequency;
        }

        public double GetFrequency() => this.frequency;

        public bool IsNotifying()
        {
            if (currentIter == 0)
            {
                return true;
            }
            return false;
        }

        public void Step()
        {

            currentIter++;
            if (currentIter == frequency)
            {
                currentIter = 0;
            }
        }

        public bool StepCheck(double newStep)
        {
            if ((newStep - lastStep) % frequency == 0)
            {
                this.lastStep = newStep;
                return true;
            }

            return false;
        }
    }
}
