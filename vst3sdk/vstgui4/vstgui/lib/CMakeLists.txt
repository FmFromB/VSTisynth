##########################################################################################
# VSTGUI Library
##########################################################################################
set(target vstgui)

set(${target}_common_sources 
    animation/animations.cpp
    animation/animations.h
    animation/animator.cpp
    animation/animator.h
    animation/ianimationtarget.h
    animation/itimingfunction.h
    animation/timingfunctions.cpp
    animation/timingfunctions.h
    algorithm.h
    cbitmap.cpp
    cbitmap.h
    cbitmapfilter.cpp
    cbitmapfilter.h
    cbuttonstate.h
    ccolor.cpp
    ccolor.h
    cdatabrowser.cpp
    cdatabrowser.h
    cdrawcontext.cpp
    cdrawcontext.h
    cdrawdefs.h
    cdrawmethods.cpp
    cdrawmethods.h
    cdropsource.cpp
    cdropsource.h
    cfileselector.cpp
    cfileselector.h
    cfont.cpp
    cfont.h
    cframe.cpp
    cframe.h
    cgradient.h
    cgradientview.cpp
    cgradientview.h
    cgraphicspath.cpp
    cgraphicspath.h
    cgraphicstransform.h
    clayeredviewcontainer.cpp
    clayeredviewcontainer.h
    clinestyle.cpp
    clinestyle.h
    coffscreencontext.cpp
    coffscreencontext.h
    controls/cautoanimation.cpp
    controls/cautoanimation.h
    controls/cbuttons.cpp
    controls/cbuttons.h
    controls/ccolorchooser.cpp
    controls/ccolorchooser.h
    controls/ccontrol.cpp
    controls/ccontrol.h
    controls/cfontchooser.cpp
    controls/cfontchooser.h
    controls/cknob.cpp
    controls/cknob.h
    controls/clistcontrol.cpp
    controls/clistcontrol.h
    controls/cmoviebitmap.cpp
    controls/cmoviebitmap.h
    controls/cmoviebutton.cpp
    controls/cmoviebutton.h
    controls/coptionmenu.cpp
    controls/coptionmenu.h
    controls/cparamdisplay.cpp
    controls/cparamdisplay.h
    controls/cscrollbar.cpp
    controls/cscrollbar.h
    controls/csearchtextedit.cpp
    controls/csearchtextedit.h
    controls/csegmentbutton.cpp
    controls/csegmentbutton.h
    controls/cslider.cpp
    controls/cslider.h
    controls/cspecialdigit.cpp
    controls/cspecialdigit.h
    controls/csplashscreen.cpp
    controls/csplashscreen.h
    controls/cstringlist.cpp
    controls/cstringlist.h
    controls/cswitch.cpp
    controls/cswitch.h
    controls/ctextedit.cpp
    controls/ctextedit.h
    controls/ctextlabel.cpp
    controls/ctextlabel.h
    controls/cvumeter.cpp
    controls/cvumeter.h
    controls/cxypad.cpp
    controls/cxypad.h
    controls/icommandmenuitemtarget.h
    controls/icontrollistener.h
    controls/ioptionmenulistener.h
    controls/itexteditlistener.h
    controls/itextlabellistener.h
    copenglview.cpp
    copenglview.h
    cpoint.cpp
    cpoint.h
    crect.cpp
    crect.h
    cresourcedescription.h
    crowcolumnview.cpp
    crowcolumnview.h
    cscrollview.cpp
    cscrollview.h
    cshadowviewcontainer.cpp
    cshadowviewcontainer.h
    csplitview.cpp
    csplitview.h
    cstring.cpp
    cstring.h
    ctabview.cpp
    ctabview.h
    ctooltipsupport.cpp
    ctooltipsupport.h
    cview.cpp
    cview.h
    cviewcontainer.cpp
    cviewcontainer.h
    cvstguitimer.cpp
    cvstguitimer.h
    dragging.h
    dispatchlist.h
    genericstringlistdatabrowsersource.cpp
    genericstringlistdatabrowsersource.h
    idatabrowserdelegate.h
    idatapackage.h
    idependency.h
    ifocusdrawing.h
    iscalefactorchangedlistener.h
    itouchevent.h
    iviewlistener.h
    malloc.h
    optional.h
    platform/iplatformbitmap.h
    platform/iplatformfont.h
    platform/iplatformframe.h
    platform/iplatformframecallback.h
    platform/iplatformopenglview.h
    platform/iplatformoptionmenu.h
    platform/iplatformresourceinputstream.h
    platform/iplatformstring.h
    platform/iplatformtextedit.h
    platform/iplatformtimer.h
    platform/iplatformviewlayer.h
    platform/platform_macos.h
    platform/platform_win32.h
    platform/platform_x11.h
    platform/std_unorderedmap.h
    platform/common/genericoptionmenu.cpp
    platform/common/genericoptionmenu.h
    vstguibase.h
    vstguidebug.cpp
    vstguidebug.h
    vstguifwd.h
    vstkeycode.h
)

  ##########################################################################################
set(${target}_mac_sources
    platform/common/fileresourceinputstream.cpp
    platform/common/fileresourceinputstream.h
    platform/common/genericoptionmenu.cpp
    platform/common/genericoptionmenu.h
    platform/common/generictextedit.cpp
    platform/common/generictextedit.h
    platform/mac/carbon/hiviewframe.cpp
    platform/mac/carbon/hiviewframe.h
    platform/mac/carbon/hiviewoptionmenu.cpp
    platform/mac/carbon/hiviewoptionmenu.h
    platform/mac/carbon/hiviewtextedit.cpp
    platform/mac/carbon/hiviewtextedit.h
    platform/mac/caviewlayer.h
    platform/mac/caviewlayer.mm
    platform/mac/cfontmac.h
    platform/mac/cfontmac.mm
    platform/mac/cgbitmap.cpp
    platform/mac/cgbitmap.h
    platform/mac/cgdrawcontext.cpp
    platform/mac/cgdrawcontext.h
    platform/mac/cocoa/autoreleasepool.h
    platform/mac/cocoa/autoreleasepool.mm
    platform/mac/cocoa/cocoahelpers.h
    platform/mac/cocoa/cocoahelpers.mm
    platform/mac/cocoa/cocoaopenglview.h
    platform/mac/cocoa/cocoaopenglview.mm
    platform/mac/cocoa/cocoatextedit.h
    platform/mac/cocoa/cocoatextedit.mm
    platform/mac/cocoa/nsviewdraggingsession.h
    platform/mac/cocoa/nsviewdraggingsession.mm
    platform/mac/cocoa/nsviewframe.h
    platform/mac/cocoa/nsviewframe.mm
    platform/mac/cocoa/nsviewoptionmenu.h
    platform/mac/cocoa/nsviewoptionmenu.mm
    platform/mac/macclipboard.h
    platform/mac/macclipboard.mm
    platform/mac/macfileselector.mm
    platform/mac/macglobals.cpp
    platform/mac/macglobals.h
    platform/mac/macstring.h
    platform/mac/macstring.mm
    platform/mac/mactimer.cpp
    platform/mac/quartzgraphicspath.cpp
    platform/mac/quartzgraphicspath.h
    platform/win32
    platform/linux
    platform/mac/ios
    ../doxygen
)

  ##########################################################################################
set(${target}_win32_sources
    platform/common/fileresourceinputstream.cpp
    platform/common/fileresourceinputstream.h
    platform/win32/direct2d/d2dbitmap.cpp
    platform/win32/direct2d/d2dbitmap.h
    platform/win32/direct2d/d2ddrawcontext.cpp
    platform/win32/direct2d/d2ddrawcontext.h
    platform/win32/direct2d/d2dfont.cpp
    platform/win32/direct2d/d2dfont.h
    platform/win32/direct2d/d2dgraphicspath.cpp
    platform/win32/direct2d/d2dgraphicspath.h
    platform/win32/win32bitmapbase.h
    platform/win32/win32dll.h
    platform/win32/win32datapackage.cpp
    platform/win32/win32datapackage.h
    platform/win32/win32dragging.cpp
    platform/win32/win32dragging.h
    platform/win32/win32frame.cpp
    platform/win32/win32frame.h
    platform/win32/win32openglview.cpp
    platform/win32/win32openglview.h
    platform/win32/win32optionmenu.cpp
    platform/win32/win32optionmenu.h
    platform/win32/win32support.cpp
    platform/win32/win32support.h
    platform/win32/win32textedit.cpp
    platform/win32/win32textedit.h
    platform/win32/winfileselector.cpp
    platform/win32/winstring.cpp
    platform/win32/winstring.h
    platform/win32/wintimer.cpp
)

 ##########################################################################################
set(${target}_linux_sources
    platform/common/fileresourceinputstream.cpp
    platform/common/fileresourceinputstream.h
    platform/common/generictextedit.cpp
    platform/common/generictextedit.h
    platform/common/stb_textedit.h
    platform/linux/cairobitmap.cpp
    platform/linux/cairobitmap.h
    platform/linux/cairocontext.cpp
    platform/linux/cairocontext.h
    platform/linux/cairofont.cpp
    platform/linux/cairofont.h
    platform/linux/cairogradient.cpp
    platform/linux/cairogradient.h
    platform/linux/cairopath.cpp
    platform/linux/cairopath.h
    platform/linux/cairoutils.h
    platform/linux/linuxstring.cpp
    platform/linux/linuxstring.h
    platform/linux/x11fileselector.cpp
    platform/linux/x11frame.cpp
    platform/linux/x11frame.h
    platform/linux/x11platform.cpp
    platform/linux/x11platform.h
    platform/linux/x11timer.cpp
    platform/linux/x11timer.h
    platform/linux/x11utils.cpp
    platform/linux/x11utils.h
)

##########################################################################################
if(CMAKE_HOST_APPLE)
    set(${target}_sources ${${target}_common_sources} ${${target}_mac_sources})
endif()

##########################################################################################
if(MSVC)
    set(${target}_sources ${${target}_common_sources} ${${target}_win32_sources})
endif()

##########################################################################################
# Linux
if(LINUX)
    set(${target}_sources ${${target}_common_sources} ${${target}_linux_sources})
endif()

##########################################################################################
add_library(${target} STATIC ${${target}_sources})

target_compile_definitions(${target} ${VSTGUI_COMPILE_DEFINITIONS})
vstgui_set_cxx_version(${target} 11)
vstgui_source_group_by_folder(${target})

if(LINUX)
    target_include_directories(${target} PRIVATE ${X11_INCLUDE_DIR})
    target_include_directories(${target} PRIVATE ${FREETYPE_INCLUDE_DIRS})
    target_link_libraries(${target} PRIVATE ${LINUX_LIBRARIES})
endif()
