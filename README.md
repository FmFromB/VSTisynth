# VSTi синтезатор

Для создания используется

1) #NAudio
2) #AudioPlugSharp
3) #VST .NET

#Чтобы запустить:

1) Перейти в папку vstbuild и открыть vstsdk.sln
2) Выбрать конфигурацию решения Release x64 (По умолчанию будет Debug Any CPU)
3) ПКМ на ALL_BUILD и нажать "Пересобрать"
4) Как закончится перейти в начальную папку и открыть AudioPlugSharp.sln
5) Снова пункт 2
6) Пересобрать все проекты из папки vstsdk (#ПО ОТДЕЛЬНОСТИ!!!)
7) Пересобрать AudioPlugSharpVst
8) Пересобрать WPFExample в папке ExamplePlugins
9) Чтобы затестить изменения нужно пересобрать WPFExample

#Чтобы открыть в фл

1) Открыть корневую папку проекта (в проводнике)
2) Скопировать содерживмое папки: "WPFExample -> bin -> Release -> net6.0-windows"
3) Вставить в папку где хранятся VST3 для (по умолчанию "C:\Program Files (x86)\Common Files\VST3")
4) Открыть фл
5) Options -> File settings -> Manage plugins -> Find installed plugins
6) Слева в проводнике фл перейти в Plugin database -> Installed -> Generators
7) Новый плагин будет подсвечен оранжевым




