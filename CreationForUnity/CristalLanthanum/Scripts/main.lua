local AnimObject = vci.assets.GetTransform("Model")
local Animator = AnimObject.GetAnimation()

function onUse(use)
     Animator.PlayFromName('StarOnAnimation', true)
end

function onUnuse(use)
     Animator.PlayFromName('StarOffAnimation', true)
end
