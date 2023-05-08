import xml.dom.minidom
import sys
#Sets up for parsing
def parse_File(filename):
    file = xml.dom.minidom.parse(filename)
    doc = file.documentElement
    return doc

#Class which rocketSimFinal.py uses to get values from the XML file created by the C# UI
class Data:
    def __init__(self, filename):
        doc = parse_File(filename)
        self.__DryMass = doc.getElementsByTagName("dryMass")[0].firstChild.nodeValue
        self.__Pressure = doc.getElementsByTagName("pressure")[0].firstChild.nodeValue
        self.__Mass = doc.getElementsByTagName("mass")[0].firstChild.nodeValue
        self.__Drag = doc.getElementsByTagName("drag")[0].firstChild.nodeValue
        self.__modelTime = doc.getElementsByTagName("modelName")[0].firstChild.nodeValue
        self.__massTime = doc.getElementsByTagName("massVTime")[0].firstChild.nodeValue
        self.__thrustTime = doc.getElementsByTagName("thrustVTime")[0].firstChild.nodeValue
        self.__time = doc.getElementsByTagName("time")[0].firstChild.nodeValue

    #Returns the values
    def get_DryMass(self):
        return float(self.__DryMass)
    def get_Pressure(self):
        return float(self.__Pressure)
    def get_Mass(self):
        return float(self.__Mass)
    def get_Drag(self):
        return float(self.__Drag)
    def get_modelTime(self):
        return float(self.__modelTime)
    def get_massTime(self):
        return float(self.__massTime)
    def get_thrustTime(self):
        return float(self.__thrustTime)
    def get_time(self):
        return float(self.__time)

if __name__ == '__main__':
    if len(sys.argv) < 2:
        print("Please provide the filename as a command line argument.")
        sys.exit(1)
    file = sys.argv[1]

    obj = Data(file)
    print(obj.get_DryMass())
    print(obj.get_Pressure())
    print(obj.get_Mass())
    print(obj.get_Drag())
    print(obj.get_modelTime())
    print(obj.get_massTime())
    print(obj.get_thrustTime())
    print(obj.get_time())



