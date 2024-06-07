using Bonsai;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using OpenCV.Net;
using static TorchSharp.torch;
using System.Drawing;

namespace MidasTorchModel
{
    [Combinator]
    [Description("")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    public class ToImage
    {

        public IObservable<IplImage> Process(IObservable<Tensor> source)
        {
            return source.Select(tensor => {
                return OpenCVImager.ToImage(ByteTensor(tensor));
            });
        }
    }
}