# Bonsai TorchSharp MiDaS Model - GPU example on Linux

This repo provides a working example for how to download a pretrained model from [torch hub](https://pytorch.org/hub/), and run online inference on the GPU using Bonsai on Linux.

# Dependencies

* Bonsai installation on Linux [follow guide here](https://github.com/orgs/bonsai-rx/discussions/1101)
* [dotnet-sdk (v8)](https://dotnet.microsoft.com/en-us/download)
* libtorch libraries installed globally
* [python v3.10](https://www.python.org/downloads/)

>[!TIP]
>It is highly recommended to use containerized environments for this setup (e.g. python venv, bonsai environment tool, docker environments)

# How to run

Clone and cd to the repo. Afterwards, create a python virtual environment with torch installed. You can do this with:

```
python3 -m venv .venv
source .venv/bin/activate
pip install -r requirements.txt
```

Next, run the python script to save the MiDaS_small model to a torch script file which will be used for running online inference. You can do this with:

```
python torchhub_MiDaS_download.py
```

Afterwards, you should see a file called `MiDaS.pt` inside of the `models` directory. You will need to create a bonsai environment and install the correct packages. To do this, you should first install the [Bonsai - linux environment template](https://github.com/ncguilbeault/bonsai-linux-environment-template), and then create the environment using:

```
dotnet new bonsaienvl --allow-scripts
cp Bonsai.config .bonsai
source .bonsai/activate
```

Finally, launch bonsai with the `bonsai-torchsharp-MiDaS-gpu.bonsai` workflow and run using:

```
bonsai workflows/bonsai-torchsharp-MiDaS-gpu.bonsai --start
```

Open up the `MiDaSInference` visualizer to see the models output in real-time!