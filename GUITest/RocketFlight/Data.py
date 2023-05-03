import xml.dom.minidom
import sys

def parse_File(file):
    file = xml.dom.minidom.parse(file)
    doc = file.documentElement
    return doc

class Data:
    def __init__(self, filename):
        doc = parse_File(filename)
        root = doc.documentElement
        self.__DryMass = float(root.getElementByTagName("dryMass")[0].firstChild.nodevalue)
        self.__Pressure = float(root.getElementByTagName("pressure")[0].firstChild.nodevalue)
        self.__Mass = float(root.getElementByTagName("mass")[0].firstChild.nodevalue)
        self.__Drag = float(root.getElementByTagName("drag")[0].firstChild.nodevalue)
        self.__modelName = root.getElementByTagName("modelName")[0].firstChild.nodevalue
        self.__massVTime = root.getElementByTagName("massVTime")[0].firstChild.nodevalue
        self.__thrustTime = root.getElementByTagName("thrustVTime")[0].firstChild.nodevalue

    def get_DryMass(self):
        return self.__DryMass
    def get_Pressure(self):
        return self.__Pressure
    def get_Mass(self):
        return self.__Mass
    def get_Drag(self):
        return self.__Drag
    def get_modelTime(self):
        return self.__modelTime
    def get_massTime(self):
        return self.__massTime
    def get_thrustTime(self):
        return self.__thrustTime


if __name__ == '__main__':
    if len(sys.argv) < 2:
        print("Please provide the filename as a command line argument.")
        sys.exit(1)
    file = sys.argv[1]


file = ".\FlightModel-Fred.xml"
obj = Data(file)
print(obj.get_DryMass())
print(obj.get_Pressure())
print(obj.get_Mass())
print(obj.get_Drag())
print(obj.get_modelTime())
print(obj.get_massTime())
print(obj.get_thrustTime())

