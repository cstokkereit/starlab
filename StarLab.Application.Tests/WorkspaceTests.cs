using StarLab.Application.Workspace;

namespace StarLab.Application.Model
{
    public class WorkspaceTests
    {
        private static WorkspaceDTO? dto;

        [SetUp]
        public static void Initialise()
        {
            var f1 = new FolderDTO()
            {
                Path = "Workspace/Folder-1",
                Expanded = true
            };

            var f2 = new FolderDTO()
            {
                Path = "Workspace/Folder-2",
                Expanded = true
            };

            var f11 = new FolderDTO()
            {
                Path = "Workspace/Folder-1/Folder-1",
                Expanded = true
            };

            var f12 = new FolderDTO()
            {
                Path = "Workspace/Folder-1/Folder-2",
                Expanded = true
            };

            var f21 = new FolderDTO()
            {
                Path = "Workspace/Folder-2/Folder-1",
                Expanded = true
            };

            var f22 = new FolderDTO()
            {
                Path = "Workspace/Folder-2/Folder-2",
                Expanded = true
            };

            var f221 = new FolderDTO()
            {
                Path = "Workspace/Folder-2/Folder-2/Folder-1",
                Expanded = true
            };

            var f222 = new FolderDTO()
            {
                Path = "Workspace/Folder-2/Folder-2/Folder-2",
                Expanded = true
            };

            var f223 = new FolderDTO()
            {
                Path = "Workspace/Folder-2/Folder-2/Folder-3",
                Expanded = true
            };

            dto = new WorkspaceDTO();

            dto.Folders.Add(f1);
            dto.Folders.Add(f2);
            dto.Folders.Add(f11);
            dto.Folders.Add(f12);
            dto.Folders.Add(f21);
            dto.Folders.Add(f22);
            dto.Folders.Add(f221);
            dto.Folders.Add(f222);
            dto.Folders.Add(f223);
        }

        [Test]
        public void TestDefaultConstructorWithDTO()
        {
            var ws = new Workspace(new WorkspaceDTO());

            Assert.That(ws, Is.Not.Null);
        }

        [Test]
        public void TestDefaultConstructor()
        {
            var ws = new Workspace();

            Assert.That(ws, Is.Not.Null);
        }

        [Test]
        public void TestDeleteFolder()
        {
            var ws = new Workspace(dto);

            ws.DeleteFolder("Workspace/Folder-2/Folder-2");

            var f = new List<IFolder>(ws.Folders);

            Assert.Multiple(() =>
            {
                Assert.That(f, Has.Count.EqualTo(5));

                Assert.That(f[0].Name, Is.EqualTo("Folder-1"));
                Assert.That(f[0].Path, Is.EqualTo("Workspace/Folder-1"));

                Assert.That(f[1].Name, Is.EqualTo("Folder-2"));
                Assert.That(f[1].Path, Is.EqualTo("Workspace/Folder-2"));

                Assert.That(f[2].Name, Is.EqualTo("Folder-1"));
                Assert.That(f[2].Path, Is.EqualTo("Workspace/Folder-1/Folder-1"));

                Assert.That(f[3].Name, Is.EqualTo("Folder-2"));
                Assert.That(f[3].Path, Is.EqualTo("Workspace/Folder-1/Folder-2"));

                Assert.That(f[4].Name, Is.EqualTo("Folder-1"));
                Assert.That(f[4].Path, Is.EqualTo("Workspace/Folder-2/Folder-1"));
            });
        }

        [Test]
        public void TestGetFolder()
        {
            var ws = new Workspace(dto);

            var f = ws.GetFolder("Workspace/Folder-1");

            Assert.Multiple(() =>
            {
                Assert.That(f, Is.Not.Null);
                Assert.That(f.Name, Is.EqualTo("Folder-1"));
                Assert.That(f.Path, Is.EqualTo("Workspace/Folder-1"));
            });
        }

        [Test]
        public void TestGetFolders()
        {
            var ws = new Workspace(dto);

            var f = new List<IFolder>(ws.Folders);

            Assert.Multiple(() =>
            {
                Assert.That(f, Has.Count.EqualTo(9));

                Assert.That(f[0].Name, Is.EqualTo("Folder-1"));
                Assert.That(f[0].Path, Is.EqualTo("Workspace/Folder-1"));

                Assert.That(f[1].Name, Is.EqualTo("Folder-2"));
                Assert.That(f[1].Path, Is.EqualTo("Workspace/Folder-2"));

                Assert.That(f[2].Name, Is.EqualTo("Folder-1"));
                Assert.That(f[2].Path, Is.EqualTo("Workspace/Folder-1/Folder-1"));

                Assert.That(f[3].Name, Is.EqualTo("Folder-2"));
                Assert.That(f[3].Path, Is.EqualTo("Workspace/Folder-1/Folder-2"));

                Assert.That(f[4].Name, Is.EqualTo("Folder-1"));
                Assert.That(f[4].Path, Is.EqualTo("Workspace/Folder-2/Folder-1"));

                Assert.That(f[5].Name, Is.EqualTo("Folder-2"));
                Assert.That(f[5].Path, Is.EqualTo("Workspace/Folder-2/Folder-2"));

                Assert.That(f[6].Name, Is.EqualTo("Folder-1"));
                Assert.That(f[6].Path, Is.EqualTo("Workspace/Folder-2/Folder-2/Folder-1"));

                Assert.That(f[7].Name, Is.EqualTo("Folder-2"));
                Assert.That(f[7].Path, Is.EqualTo("Workspace/Folder-2/Folder-2/Folder-2"));

                Assert.That(f[8].Name, Is.EqualTo("Folder-3"));
                Assert.That(f[8].Path, Is.EqualTo("Workspace/Folder-2/Folder-2/Folder-3"));
            });
        }

        public void TestGetFoldersWhenEmpty()
        {
            var ws = new Workspace();

            var f = new List<IFolder>(ws.Folders);

            Assert.Multiple(() =>
            {
                Assert.That(ws.Folders, Is.Not.Null);
                Assert.That(f, Has.Count.EqualTo(0));
            });
        }

        [Test]
        public void TestGetSubFolder()
        {
            var ws = new Workspace(dto);

            var f = ws.GetFolder("Workspace/Folder-2/Folder-1");

            Assert.Multiple(() =>
            {
                Assert.That(f, Is.Not.Null);
                Assert.That(f.Name, Is.EqualTo("Folder-1"));
                Assert.That(f.Path, Is.EqualTo("Workspace/Folder-2/Folder-1"));
            });
        }

        [Test]
        public void TestRenameFolder()
        {
            var ws = new Workspace(dto);

            ws.RenameFolder(ws.GetFolder("Workspace/Folder-2"), "Folder-3");

            var f = new List<IFolder>(ws.Folders);

            Assert.Multiple(() =>
            {
                Assert.That(f, Has.Count.EqualTo(9));

                Assert.That(f[0].Name, Is.EqualTo("Folder-1"));
                Assert.That(f[0].Path, Is.EqualTo("Workspace/Folder-1"));

                Assert.That(f[1].Name, Is.EqualTo("Folder-3"));
                Assert.That(f[1].Path, Is.EqualTo("Workspace/Folder-3"));

                Assert.That(f[2].Name, Is.EqualTo("Folder-1"));
                Assert.That(f[2].Path, Is.EqualTo("Workspace/Folder-1/Folder-1"));

                Assert.That(f[3].Name, Is.EqualTo("Folder-2"));
                Assert.That(f[3].Path, Is.EqualTo("Workspace/Folder-1/Folder-2"));

                Assert.That(f[4].Name, Is.EqualTo("Folder-1"));
                Assert.That(f[4].Path, Is.EqualTo("Workspace/Folder-3/Folder-1"));

                Assert.That(f[5].Name, Is.EqualTo("Folder-2"));
                Assert.That(f[5].Path, Is.EqualTo("Workspace/Folder-3/Folder-2"));

                Assert.That(f[6].Name, Is.EqualTo("Folder-1"));
                Assert.That(f[6].Path, Is.EqualTo("Workspace/Folder-3/Folder-2/Folder-1"));

                Assert.That(f[7].Name, Is.EqualTo("Folder-2"));
                Assert.That(f[7].Path, Is.EqualTo("Workspace/Folder-3/Folder-2/Folder-2"));

                Assert.That(f[8].Name, Is.EqualTo("Folder-3"));
                Assert.That(f[8].Path, Is.EqualTo("Workspace/Folder-3/Folder-2/Folder-3"));

                // TODO Documents
            });
        }

        [Test]
        public void TestRenameSubFolder()
        {
            var ws = new Workspace(dto);

            var f = ws.GetFolder("Workspace/Folder-2/Folder-1");

            ws.RenameFolder(f, "Folder-3");

            var f3 = ws.GetFolder("Workspace/Folder-2/Folder-3");

            Assert.Multiple(() =>
            {
                Assert.That(f3, Is.Not.Null);
                Assert.That(f3.Name, Is.EqualTo("Folder-3"));
                Assert.That(f3.Path, Is.EqualTo("Workspace/Folder-2/Folder-3"));
            });
        }
    }
}