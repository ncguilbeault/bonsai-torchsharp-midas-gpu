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
    public class ToTensor
    {
        public IObservable<Tensor> Process(IObservable<IplImage> source)
        {
            return source.Select(image => {
                return OpenCVImager.ToTensor(image);
            });
        }
    }
}
