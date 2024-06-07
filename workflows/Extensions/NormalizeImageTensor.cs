using Bonsai;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using OpenCV.Net;
using static TorchSharp.torch;

namespace MidasTorchModel
{
    [Combinator]
    [Description("")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    public class NormalizeImageTensor
    {
        public IObservable<Tensor> Process(IObservable<Tensor> source)
        {
            return source.Select(tensor => {
                return Utils.NormalizeImageTensor(tensor);
            });
        }
    }
}