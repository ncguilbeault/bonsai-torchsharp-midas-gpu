import torch

model_type = "MiDaS_small"
midas = torch.hub.load("intel-isl/MiDaS", model_type, pretrained=True)
midas.eval()

example_input = torch.rand(1, 3, 256, 256)

with torch.no_grad():
    o1 = midas(example_input)
    traced_model = torch.jit.trace(midas, example_input)
    traced_model.save("model/MiDaS.pt")
