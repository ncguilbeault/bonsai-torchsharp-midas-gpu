using Bonsai;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using static TorchSharp.torch;
using System.Xml.Serialization;

namespace MidasTorchModel
{
    [Combinator]
    [Description("")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [DefaultProperty("Device")]
    public class MidasInference
    {

        [XmlIgnore]
        public Device Device { get; set; }

        public string ModelPath { get; set; } = "MiDaS.pt";

        private jit.ScriptModule model;

        public IObservable<Tensor> Process(IObservable<Tensor> source)
        {
            model = jit.load(ModelPath, Device);
            return source.Select(tensor => {
                using (no_grad())
                {
                    return (Tensor)model.forward(tensor);
                }
            });
        }
    }
}

