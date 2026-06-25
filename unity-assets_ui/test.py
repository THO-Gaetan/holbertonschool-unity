import os
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
           

def checkFont(nodes):
    fontID = checkGameObjectName(nodes, "OptionsText")
    
    font_file_path = 'Assets/Fonts/Changa/Changa-Medium.ttf.meta'
    filename = 'Changa-Medium.asset'

    font_asset_guid = None
    for node in nodes:
        if 'MonoBehaviour' in node.keys() and node['MonoBehaviour']['m_GameObject']['fileID'] == fontID:
            if 'm_fontAsset' in node['MonoBehaviour'].keys():
                font_asset_guid = node['MonoBehaviour']['m_fontAsset']['guid']
                # print("Font asset GUID in GameObject: ", font_asset_guid)  # Debug print

    if font_asset_guid is None:
        print("Font asset not found in GameObject")
        return

    # Get the font asset file
    path = 'Assets/Fonts/Changa'
    filepath = None

    for root, dirs, files in os.walk(path):
        if filename in files:
            filepath = os.path.join(root, filename)

    if filepath is None:
        print("Font asset file not found")
        return

    # Get the GUID of the source font file from the font asset
    source_font_file_guid = None
    with open(filepath, 'r') as f:
        for line in f:
            if 'm_SourceFontFileGUID' in line:
                source_font_file_guid = line.split(': ')[1].strip()
                break

    if source_font_file_guid is None:
        print("Source font file GUID not found in the font asset")
        return

    # print("Source font file GUID in asset file: ", source_font_file_guid)  # Debug print

    # Look for the actual source font file we are looking for and grab its GUID
    actual_source_font_guid = None

    with open(font_file_path, 'r') as f:
        f.readline()  # Skip first line
        line = f.readline()  # Read second line
        actual_source_font_guid = line.split(': ')[1].strip()  # Extract GUID

    if actual_source_font_guid is None:
        print("Actual source font GUID not found")
        return

    # print("Actual source font GUID: ", actual_source_font_guid)  # Debug print

    # Compare the GUID from source font file in asset to actual source font file
    if source_font_file_guid == actual_source_font_guid:
        print("Font: OK")
    else:
        print("Font incorrect")


def checkText(nodes):
    textID = checkGameObjectName(nodes, "OptionsText")
    bgID = checkGameObjectName(nodes, "OptionsButton")

    bgIDRT = None
    for node in nodes:
         if 'RectTransform' in node.keys() and 'm_GameObject' in node['RectTransform'].keys():
            if node['RectTransform']['m_GameObject']['fileID'] == bgID:
                bgIDRT = node['ID']

    for node in nodes:
        if 'RectTransform' in node.keys() and 'm_GameObject' in node['RectTransform'].keys():
            if node['RectTransform']['m_GameObject']['fileID'] == textID:
                if node['RectTransform']['m_Father']['fileID'] == bgIDRT:
                    print("Parent / child: OK")
                else:
                    print("Text is not child of OptionsButton")

    checkFont(nodes)                    

    for node in nodes:
        if 'MonoBehaviour' in node.keys() and node['MonoBehaviour']['m_GameObject']['fileID'] == textID:
            if node['MonoBehaviour']['m_Color']['r'] == 1 and node['MonoBehaviour']['m_Color']['g'] == 1 and node['MonoBehaviour']['m_Color']['b'] == 1:
                print("Text color: OK")
            else:
                print("Text color incorrect")
            if 'm_fontSize' in node['MonoBehaviour'].keys() and 'm_HorizontalAlignment' in node['MonoBehaviour'].keys() and 'm_VerticalAlignment' in node['MonoBehaviour'].keys() and 'm_overflowMode' in node['MonoBehaviour'].keys():
                if node['MonoBehaviour']['m_fontSize'] == 36 and node['MonoBehaviour']['m_HorizontalAlignment'] == 2 and node['MonoBehaviour']['m_VerticalAlignment'] == 512 and node['MonoBehaviour']['m_overflowMode'] == 0:
                    print("Font settings: OK")
                else:
                    print("Font settings incorrect")
            if node['MonoBehaviour']['m_text'] == 'Options':
                print("Default text: OK")
            else:
                print("Default text incorrect")

    return (textID)



if __name__ == "__main__":
    checkText(loadYAML('Assets/Prefabs/OptionsButton.prefab'))