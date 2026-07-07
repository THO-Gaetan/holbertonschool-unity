import yaml

def removeUnityTagAlias(filepath):
    """ removes unnecessary Unity tags and adds ID to node"""
    result = str()

    with open(filepath) as srcFile:
        for lineNumber,line in enumerate(srcFile.readlines()): 
            if line.startswith('--- !u!'):          
                result += '\n--- ' + line.split(' ')[2]   # remove the tag, but keep file ID
                result += '\nID: ' + line.split('&')[1]   # add file ID
            else:
                result += line

    return (result)


def loadYAML(filepath):
    """ loads nodes from YAML and appends to list """
    data = removeUnityTagAlias(filepath)
    nodes = list()

    for data in yaml.load_all(data, Loader=yaml.FullLoader):
        nodes.append(data)
    
    return (nodes)


def checkGameObjectName(nodes, name):
    objID = None
    for node in nodes:
        if 'GameObject' in node.keys() and 'm_Name' in node['GameObject'].keys() and node['GameObject']['m_Name'] == name:
            objID = node['ID']

    if objID == None:
        print("GameObject \'" + name + "\' not found")

    return(objID)

def checkRectTransform(nodes):
    """ checks RectTransform values """

    objID = checkGameObjectName(nodes, "Level02")

    for node in nodes:
        if 'RectTransform' in node.keys() and 'm_GameObject' in node['RectTransform'].keys() and node['RectTransform']['m_GameObject']['fileID'] == objID:
            if node['RectTransform']['m_SizeDelta']['x'] == 120 and node['RectTransform']['m_SizeDelta']['y'] == 130:
                print("Rect size: OK")
            else:
                print("Rect size incorrect")


def checkMonoBehaviour(nodes):
    """ checks if GameObject has correct MonoBehaviour """

    # checkRectTransform(nodes)

    objID = checkGameObjectName(nodes, "Level02")

    guidNormal = None
    for node in nodes:
        if 'MonoBehaviour' in node.keys() and 'm_Sprite' in node['MonoBehaviour'].keys() and node['MonoBehaviour']['m_GameObject']['fileID'] == objID:
            guidNormal = node['MonoBehaviour']['m_Sprite']['guid']

    flag = False
    filename = 'Assets/Textures/UI/button-level02.png.meta'
    with open(filename, 'r') as f:
        for line in f:
            if guidNormal in line:
                print("Image sprite: OK")
                flag = True
    if flag == False:
        print("Image sprite incorrect")
    
    guidHighlight = None
    guidPressed = None
    for node in nodes:
        if 'MonoBehaviour' in node.keys() and 'm_SpriteState' in node['MonoBehaviour'].keys() and node['MonoBehaviour']['m_GameObject']['fileID'] == objID:
            guidHighlight = node['MonoBehaviour']['m_SpriteState']['m_HighlightedSprite']['guid']
            guidPressed = node['MonoBehaviour']['m_SpriteState']['m_PressedSprite']['guid']

    if guidNormal == guidHighlight == guidPressed:
        print("Highlight / Pressed Sprite: OK")
    else:
        print("Highlight / Pressed Sprite incorrect")


if __name__ == "__main__":
    checkMonoBehaviour(loadYAML('Assets/Scenes/MainMenu.unity'))