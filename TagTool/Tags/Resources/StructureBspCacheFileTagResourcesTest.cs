using System.Collections.Generic;
using TagTool.Common;
using TagTool.Tags.Definitions;
using static TagTool.Tags.TagFieldFlags;

namespace TagTool.Tags.Resources
{
    [TagStructure(Name = "structure_bsp_cache_file_tag_resources", Size = 0x30)]
    public class StructureBspCacheFileTagResourcesTest : TagStructure
	{
        public TagBlock<ScenarioStructureBsp.SurfacesPlanes> SurfacePlanes;
        public TagBlock<ScenarioStructureBsp.Plane> Planes;
        public TagBlock<ScenarioStructureBsp.EdgeToSeamMapping> EdgeToSeams;
        public TagBlock<PathfindingDatum> PathfindingData;

        [TagStructure(Size = 0x94)]
        public class PathfindingDatum : TagStructure
		{
            public TagBlock<ScenarioStructureBsp.TagPathfindingDatum.Sector> Sectors;
            public TagBlock<ScenarioStructureBsp.TagPathfindingDatum.Link> Links;
            public TagBlock<ScenarioStructureBsp.TagPathfindingDatum.Reference> References;
            public TagBlock<ScenarioStructureBsp.TagPathfindingDatum.Bsp2dNode> Bsp2dNodes;
            public TagBlock<ScenarioStructureBsp.TagPathfindingDatum.Vertex> Vertices;
            public TagBlock<ScenarioStructureBsp.TagPathfindingDatum.ObjectReference> ObjectReferences;
            public TagBlock<ScenarioStructureBsp.TagPathfindingDatum.PathfindingHint> PathfindingHints;
            public TagBlock<ScenarioStructureBsp.TagPathfindingDatum.InstancedGeometryReference> InstancedGeometryReferences;
            public int StructureChecksum;
            public TagBlock<ScenarioStructureBsp.TagPathfindingDatum.GiantPathfindingBlock> GiantPathfinding;
            public TagBlock<ScenarioStructureBsp.TagPathfindingDatum.Seam> Seams;
            public TagBlock<ScenarioStructureBsp.TagPathfindingDatum.JumpSeam> JumpSeams;
            public TagBlock<ScenarioStructureBsp.TagPathfindingDatum.Door> Doors;

            [TagStructure(Size = 0x18)]
            public class ObjectReference : TagStructure
            {
                public ushort Flags;

                [TagField(Flags = Padding, Length = 2)]
                public byte[] Unused = new byte[2];

                public TagBlock<BspReference> Bsps;

                public int ObjectUniqueID;
                public short OriginBspIndex;
                public ScenarioObjectType ObjectType;
                public Scenario.ScenarioInstance.SourceValue Source;

                [TagStructure(Size = 0x18)]
                public class BspReference : TagStructure
                {
                    public int BspIndex;
                    public short NodeIndex;

                    [TagField(Flags = Padding, Length = 2)]
                    public byte[] Unused = new byte[2];

                    public TagBlock<ScenarioStructureBsp.TagPathfindingDatum.ObjectReference.BspReference.Bsp2dRef> Bsp2dRefs;

                    public int VertexOffset;
                }
            }

            [TagStructure(Size = 0xC)]
            public class Seam : TagStructure
			{
                public TagBlock<ScenarioStructureBsp.TagPathfindingDatum.Seam.LinkIndexBlock> LinkIndices;
            }

            [TagStructure(Size = 0x14)]
            public class JumpSeam : TagStructure
			{
                public short UserJumpIndex;
                public byte DestOnly;

                [TagField(Flags = Padding, Length = 1)]
                public byte[] Unused = new byte[1];

                public float Length;

                public TagBlock<ScenarioStructureBsp.TagPathfindingDatum.JumpSeam.JumpIndexBlock> JumpIndices;
            }
        }
    }
}