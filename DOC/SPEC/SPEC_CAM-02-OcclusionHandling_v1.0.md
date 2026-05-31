# CAM-02 - Camera Occlusion
## Dither Transparency Shader
A "Dither Transparency" Shader will be used on all occludable objects, so that they can be occluded without any visual artifacts (which is the case when using a transparency).
## Object Occlusion
A box will be projected from the camera, towards the player, and will detect any objects tagged as "Occludable".

The object will then be occluded using it's shader.