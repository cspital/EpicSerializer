# __EpicSerializer__
### *Epic Chronicles Format Serializer*

https://www.nuget.org/packages/EpicSerializer

Epic is a very popular Electronic Medical Record system, with a proprietary format for bulk importing data through their Chronicles tool. This library makes it easy to serialize arbitrary types into the Chronicles format, which resembles an `OrderedMap<int, string>`. Since Epic configurations can vary so widely among different installations, this library doesn't make any assumptions about how any particular instance is configured.

The library is designed to be scalable, it only performs introspection on your serializable types once, on it's first encounter with that type. During this encounter it creates an access plan that describes how to pull data out of the objects of this type. These plans are stored in a static thread-safe container, so all instances of the Serializer benefit.

Chronicles Format Example:<br/>
1,12345<br/>
2,Biff Jutsu<br/>
45,biffj<br/>
50,1<br/>
2301,ARNP<br/>
2302,WA<br/>
2303,ABC123456<br/>
2301,RN<br/>
2302,WA<br/>
2303,DEF789012<br/>
1,67890<br/>
2,Some Other Person<br/>
45,otherp<br/>
50,1<br/>
...

## Code
### Simple single object example.

    [EpicSerializable(MasterFile.EMP)]
    class EpicUser
    {
        [EpicRecord(Field: 1)]
        public int EpicID { get; set; }

        [EpicRecord(Field: 2)]
        public string Name { get; set; }

        [EpicRecord(Field: 45)]
        public string Username { get; set; }

        [EpicRecord(Field: 50)]
        public bool Active { get; set; }
    }

    var user = new EpicUser
    {
        EpicID = 12345,
        Name = "Biff Jutsu",
        Username = "biffj",
        Active = true
    };

    var serial = new EpicSerializer<EpicUser>();
    string serializedResult = serial.Serialize()
    Console.WriteLine(serializedResult);
    
    // Console Output
    1,12345
    2,Biff Jutsu
    45,biffj
    50,1

---

### Single object with simple repeated section.
    [EpicSerializable(MasterFile.EMP)]
    class EpicUser
    {
        [EpicRecord(Field: 1)]
        public int EpicID { get; set; }

        [EpicRecord(Field: 2)]
        public string Name { get; set; }

        [EpicRecord(Field: 45)]
        public string Username { get; set; }

        [EpicRecord(Field: 50)]
        public bool Active { get; set; }

        [EpicRepeat(Field: 100)]
        public List<string> Locations { get; set; }
    }

    var user = new EpicUser
    {
        EpicID = 12345,
        Name = "Biff Jutsu",
        Username = "biffj",
        Active = true,
        Locations = new List<string>
        {
            "Hospital",
            "Burn Ward"
        }
    };

    var serial = new EpicSerializer<EpicUser>();
    string serializedResult = serial.Serialize()
    Console.WriteLine(serializedResult);
    
    // Console Output
    1,12345
    2,Biff Jutsu
    45,biffj
    50,1
    100,Hospital
    100,Burn Ward

---

### Single object with complex repeated section.
    [EpicSerializable(MasterFile.EMP)]
    class EpicUser
    {
        [EpicRecord(Field: 1)]
        public int EpicID { get; set; }

        [EpicRecord(Field: 2)]
        public string Name { get; set; }

        [EpicRecord(Field: 45)]
        public string Username { get; set; }

        [EpicRecord(Field: 50)]
        public bool Active { get; set; }

        [EpicRepeat(Field: 100)]
        public List<string> Locations { get; set; }

        [EpicRepeat(Field: 2301)]
        public List<License> Licenses { get; set; }
    }

    [EpicSerializable(MasterFile.EMP)]
    class Alias
    {
        [EpicRecord(Field: 2301)]
        public string Type { get; set; }

        [EpicRecord(Field: 2302)]
        public string State { get; set; }

        [EpicRecord(Field: 2303)]
        public string Value { get; set; }
    }

    var user = new EpicUser
    {
        EpicID = 12345,
        Name = "Biff Jutsu",
        Username = "biffj",
        Active = true,
        Locations = new List<string>
        {
            "Hospital",
            "Burn Ward"
        },
        Licenses = new List<License>
        {
            new License
            {
                Type = "ARNP",
                State = "WA",
                Value = "ABC123456"
            },
            new License
            {
                Type = "RN",
                State = "WA",
                Value = "DEF789012"
            }
        }
    };

    var serial = new EpicSerializer<EpicUser>();
    string serializedResult = serial.Serialize()
    Console.WriteLine(serializedResult);
    
    // Console Output
    1,12345
    2,Biff Jutsu
    45,biffj
    50,1
    100,Hospital
    100,Burn Ward
    2301,ARNP
    2302,WA
    2303,ABC123456
    2301,RN
    2302,WA
    2303,DEF789012


### See tests for more detailed examples.
