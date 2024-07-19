�
�D:\Banobras\Sistemas .NET\SST3\SIVARMER\Maincore2024\Riesgos.Simefin.WindowsService\Riesgos.Simefin.WindowsService.PortfolioLoad\TimestampedPatternLayout.cs
	namespace 	
Riesgos
 
. 
Simefin 
. 
WindowsService (
.( )
PortfolioLoad) 6
{ 
public

 

class

 $
TimestampedPatternLayout

 )
:

* +
PatternLayout

, 9
{ 
private 
readonly 
string 
_serviceName  ,
=- .
$str/ E
;E F
private 
readonly 
string 

_separator  *
=+ ,
$str- r
;r s
public 
override 
string 
Header %
{ 	
get 
{ 
string 
result 
= 
$str  R
;R S
result 
= 
string 
.  
Format  &
(& '
result' -
,- .

_separator/ 9
,9 :
_serviceName; G
,G H
DateTimeI Q
.Q R
NowR U
.U V
ToStringV ^
(^ _
$str_ t
)t u
)u v
;v w
return 
result 
; 
} 
set 
{ 
} 
} 	
public 
override 
string 
Footer %
{ 	
get 
{ 
string 
result 
= 
$str  N
;N O
result 
= 
string 
.  
Format  &
(& '
result' -
,- .

_separator/ 9
,9 :
_serviceName; G
,G H
DateTimeI Q
.Q R
NowR U
.U V
ToStringV ^
(^ _
$str_ t
)t u
)u v
;v w
return   
result   
;   
}!! 
set"" 
{"" 
}"" 
}## 	
}%% 
}'' �
�D:\Banobras\Sistemas .NET\SST3\SIVARMER\Maincore2024\Riesgos.Simefin.WindowsService\Riesgos.Simefin.WindowsService.PortfolioLoad\ServiceExcelLoad.cs
	namespace 	
Riesgos
 
. 
Simefin 
. 
WindowsService (
.( )
PortfolioLoad) 6
{ 
public 

partial 
class 
ServiceExcelLoad )
:* +
ServiceBase, 7
{ 
private 
static 
readonly 
ILog  $
_log% )
=* +

LogManager, 6
.6 7
	GetLogger7 @
(@ A
$strA R
)R S
;S T
Timer 
_timer 
= 
new 
Timer  
(  !
)! "
;" #
static 
ServiceExcelLoad 
(  
)  !
{ 	
DOMConfigurator 
. 
	Configure %
(% &
)& '
;' (
} 	
public 
ServiceExcelLoad 
(  
)  !
{ 	
InitializeComponent 
(  
)  !
;! "
} 	
public!! 
void!! 
Start!! 
(!! 
string!!  
[!!  !
]!!! "
args!!# '
)!!' (
{"" 	
OnStart## 
(## 
args## 
)## 
;## 
}$$ 	
private&& 
async&& 
void&& 
OnTimerElapsed&& )
(&&) *
object&&* 0
sender&&1 7
,&&7 8
ElapsedEventArgs&&9 I
e&&J K
)&&K L
{'' 	
_log(( 
.(( 
Info(( 
((( 
$str(( *
)((* +
;((+ ,
var)) 
tokenResponse)) 
=)) 
await))  %
Task))& *
.))* +
Run))+ .
()). /
())/ 0
)))0 1
=>))2 4
GetToken))5 =
())= >
)))> ?
)))? @
;))@ A
if** 
(** 
tokenResponse** 
.** 
	IsExitoso** '
)**' (
{++ 
TokenResponse,, 
token,, #
=,,$ %
JsonConvert,,& 1
.,,1 2
DeserializeObject,,2 C
<,,C D
TokenResponse,,D Q
>,,Q R
(,,R S
tokenResponse,,S `
.,,` a
	Resultado,,a j
.,,j k
ToString,,k s
(,,s t
),,t u
),,u v
;,,v w
var-- 
result-- 
=-- 
await-- "
this--# '
.--' (!
ExecuteLoadingProcess--( =
(--= >
token--> C
.--C D
AccessToken--D O
)--O P
;--P Q
if.. 
(.. 
result.. 
... 
	IsExitoso.. $
)..$ %
{// 
_log00 
.00 
Info00 
(00 
JsonConvert00 )
.00) *
SerializeObject00* 9
(009 :
result00: @
)00@ A
)00A B
;00B C
}11 
else22 
{33 
_log44 
.44 
Error44 
(44 
JsonConvert44 *
.44* +
SerializeObject44+ :
(44: ;
result44; A
)44A B
)44B C
;44C D
}55 
}66 
else77 
{88 
_log99 
.99 
Warn99 
(99 
JsonConvert99 %
.99% &
SerializeObject99& 5
(995 6
tokenResponse996 C
)99C D
)99D E
;99E F
}:: 
};; 	
	protectedAA 
overrideAA 
voidAA 
OnStartAA  '
(AA' (
stringAA( .
[AA. /
]AA/ 0
argsAA1 5
)AA5 6
{BB 	
stringDD 
duracionIntervaloDD $
=DD% & 
ConfigurationManagerDD' ;
.DD; <
AppSettingsDD< G
[DDG H
$strDDH _
]DD_ `
;DD` a
stringEE 
minutosEnEjecucionEE %
=EE& ' 
ConfigurationManagerEE( <
.EE< =
AppSettingsEE= H
[EEH I
$strEEI Y
]EEY Z
;EEZ [
intFF 
	intervaloFF 
=FF 
ConvertFF #
.FF# $
ToInt32FF$ +
(FF+ ,
duracionIntervaloFF, =
)FF= >
*FF? @
ConvertFFA H
.FFH I
ToInt32FFI P
(FFP Q
minutosEnEjecucionFFQ c
)FFc d
;FFd e
_timerHH 
.HH 
EnabledHH 
=HH 
trueHH !
;HH! "
_timerII 
.II 
IntervalII 
=II 
	intervaloII '
;II' (
_timerJJ 
.JJ 
ElapsedJJ 
+=JJ 
OnTimerElapsedJJ ,
;JJ, -
_timerKK 
.KK 
StartKK 
(KK 
)KK 
;KK 
}LL 	
	protectedQQ 
overrideQQ 
voidQQ 
OnStopQQ  &
(QQ& '
)QQ' (
{RR 	
_timerSS 
.SS 
StopSS 
(SS 
)SS 
;SS 
_timerTT 
.TT 
DisposeTT 
(TT 
)TT 
;TT 
}UU 	
private\\ 
async\\ 
Task\\ 
<\\ 
ApiResponse\\ &
>\\& '!
ExecuteLoadingProcess\\( =
(\\= >
string\\> D
accessToken\\E P
)\\P Q
{]] 	
ApiResponse^^ 
response^^  
=^^! "
new^^# &
ApiResponse^^' 2
{^^3 4
	IsExitoso^^5 >
=^^? @
false^^A F
}^^G H
;^^H I
string__ 
baseAddress__ 
=__   
ConfigurationManager__! 5
.__5 6
AppSettings__6 A
[__A B
$str__B N
]__N O
;__O P
string`` 
endPoint`` 
=``  
ConfigurationManager`` 2
.``2 3
AppSettings``3 >
[``> ?
$str``? M
]``M N
;``N O
_logbb 
.bb 
Infobb 
(bb 
$strbb 4
)bb4 5
;bb5 6
trydd 
{ee 
usingff 
(ff 
varff 
clientff !
=ff" #
newff$ '

HttpClientff( 2
(ff2 3
)ff3 4
)ff4 5
{gg 
clienthh 
.hh 
BaseAddresshh &
=hh' (
newhh) ,
Urihh- 0
(hh0 1
baseAddresshh1 <
)hh< =
;hh= >
clientii 
.ii !
DefaultRequestHeadersii 0
.ii0 1
Acceptii1 7
.ii7 8
Clearii8 =
(ii= >
)ii> ?
;ii? @
clientjj 
.jj !
DefaultRequestHeadersjj 0
.jj0 1
Acceptjj1 7
.jj7 8
Addjj8 ;
(jj; <
newjj< ?+
MediaTypeWithQualityHeaderValuejj@ _
(jj_ `
$strjj` r
)jjr s
)jjs t
;jjt u
clientkk 
.kk !
DefaultRequestHeaderskk 0
.kk0 1
Addkk1 4
(kk4 5
$strkk5 D
,kkD E
$strkkF O
+kkP Q
accessTokenkkR ]
)kk] ^
;kk^ _
varmm 
resultmm 
=mm  
awaitmm! &
clientmm' -
.mm- .
	PostAsyncmm. 7
(mm7 8
endPointmm8 @
,mm@ A
nullmmB F
)mmF G
;mmG H
responsenn 
=nn 
awaitnn $
resultnn% +
.nn+ ,
Contentnn, 3
.nn3 4
ReadAsAsyncnn4 ?
<nn? @
ApiResponsenn@ K
>nnK L
(nnL M
)nnM N
;nnN O
ifoo 
(oo 
responseoo  
.oo  !

StatusCodeoo! +
==oo, .
HttpStatusCodeoo/ =
.oo= >
OKoo> @
)oo@ A
{pp 
_logqq 
.qq 
Infoqq !
(qq! "
responseqq" *
.qq* +
Mensajeqq+ 2
)qq2 3
;qq3 4
}rr 
elsess 
{tt 
_loguu 
.uu 
Warnuu !
(uu! "
responseuu" *
.uu* +
Mensajeuu+ 2
)uu2 3
;uu3 4
}vv 
}ww 
}xx 
catchyy 
(yy 
	Exceptionyy 
exyy 
)yy  
{zz 
response{{ 
.{{ 
	IsExitoso{{ "
={{# $
false{{% *
;{{* +
response|| 
.|| 
Mensaje||  
=||! "
$str||# Y
+||Z [
this||\ `
.||` a
GetExceptionMessage||a t
(||t u
ex||u w
)||w x
;||x y
response}} 
.}} 

StatusCode}} #
=}}$ %
HttpStatusCode}}& 4
.}}4 5

BadRequest}}5 ?
;}}? @
_log~~ 
.~~ 
Error~~ 
(~~ 
response~~ #
.~~# $
Mensaje~~$ +
)~~+ ,
;~~, -
} 
_log
�� 
.
�� 
Info
�� 
(
�� 
$str
�� 7
)
��7 8
;
��8 9
return
�� 
response
�� 
;
�� 
}
�� 	
public
�� 
async
�� 
Task
�� 
<
�� 
ApiResponse
�� %
>
��% &
GetToken
��' /
(
��/ 0
)
��0 1
{
�� 	
ApiResponse
�� 
response
��  
=
��! "
new
��# &
ApiResponse
��' 2
{
��3 4
	IsExitoso
��5 >
=
��? @
false
��A F
}
��G H
;
��H I
string
�� !
environmentVariable
�� &
=
��' ("
ConfigurationManager
��) =
.
��= >
AppSettings
��> I
[
��I J
$str
��J _
]
��_ `
;
��` a
string
�� &
environmentVariableValue
�� +
=
��, -
Environment
��. 9
.
��9 :$
GetEnvironmentVariable
��: P
(
��P Q!
environmentVariable
��Q d
)
��d e
;
��e f
if
�� 
(
�� 
string
�� 
.
�� 
IsNullOrEmpty
�� $
(
��$ %&
environmentVariableValue
��% =
)
��= >
)
��> ?
{
�� 
response
�� 
.
�� 

StatusCode
�� #
=
��$ %
HttpStatusCode
��& 4
.
��4 5

BadRequest
��5 ?
;
��? @
response
�� 
.
�� 
Mensaje
��  
=
��! "
$str
��# E
;
��E F
return
�� 
response
�� 
;
��  
}
�� 
var
�� 
userData
�� 
=
�� 
JsonConvert
�� &
.
��& '
DeserializeObject
��' 8
<
��8 9
TokenRequest
��9 E
>
��E F
(
��F G&
environmentVariableValue
��G _
)
��_ `
;
��` a
if
�� 
(
�� 
string
�� 
.
�� 
IsNullOrEmpty
�� $
(
��$ %
userData
��% -
.
��- .
UserName
��. 6
)
��6 7
||
��8 :
string
��; A
.
��A B
IsNullOrEmpty
��B O
(
��O P
userData
��P X
.
��X Y
Password
��Y a
)
��a b
)
��b c
{
�� 
response
�� 
.
�� 

StatusCode
�� #
=
��$ %
HttpStatusCode
��& 4
.
��4 5

BadRequest
��5 ?
;
��? @
response
�� 
.
�� 
Mensaje
��  
=
��! "
$str
��# e
;
��e f
return
�� 
response
�� 
;
��  
}
�� 
var
�� 

parameters
�� 
=
�� 
new
��  
{
��! "
UserName
��# +
=
��, -
userData
��. 6
.
��6 7
UserName
��7 ?
,
��? @
Password
��A I
=
��J K
userData
��L T
.
��T U
Password
��U ]
}
��^ _
;
��_ `
var
�� 
jsonData
�� 
=
�� 
JsonConvert
�� &
.
��& '
SerializeObject
��' 6
(
��6 7

parameters
��7 A
)
��A B
;
��B C
string
�� 
baseAddress
�� 
=
��  "
ConfigurationManager
��! 5
.
��5 6
AppSettings
��6 A
[
��A B
$str
��B N
]
��N O
;
��O P
string
�� 
endPoint
�� 
=
�� "
ConfigurationManager
�� 2
.
��2 3
AppSettings
��3 >
[
��> ?
$str
��? I
]
��I J
;
��J K
try
�� 
{
�� 
using
�� 
(
�� 
var
�� 
client
�� !
=
��" #
new
��$ '

HttpClient
��( 2
(
��2 3
)
��3 4
)
��4 5
{
�� 
client
�� 
.
�� 
BaseAddress
�� &
=
��' (
new
��) ,
Uri
��- 0
(
��0 1
baseAddress
��1 <
)
��< =
;
��= >
client
�� 
.
�� #
DefaultRequestHeaders
�� 0
.
��0 1
Accept
��1 7
.
��7 8
Clear
��8 =
(
��= >
)
��> ?
;
��? @
client
�� 
.
�� #
DefaultRequestHeaders
�� 0
.
��0 1
Accept
��1 7
.
��7 8
Add
��8 ;
(
��; <
new
��< ?-
MediaTypeWithQualityHeaderValue
��@ _
(
��_ `
$str
��` r
)
��r s
)
��s t
;
��t u
var
�� 
content
�� 
=
��  !
new
��" %
StringContent
��& 3
(
��3 4
jsonData
��4 <
,
��< =
Encoding
��> F
.
��F G
UTF8
��G K
,
��K L
$str
��M _
)
��_ `
;
��` a
var
�� 
result
�� 
=
��  
await
��! &
client
��' -
.
��- .
	PostAsync
��. 7
(
��7 8
endPoint
��8 @
,
��@ A
content
��B I
)
��I J
;
��J K
response
�� 
=
�� 
await
�� $
result
��% +
.
��+ ,
Content
��, 3
.
��3 4
ReadAsAsync
��4 ?
<
��? @
ApiResponse
��@ K
>
��K L
(
��L M
)
��M N
;
��N O
if
�� 
(
�� 
response
��  
.
��  !

StatusCode
��! +
==
��, .
HttpStatusCode
��/ =
.
��= >
OK
��> @
)
��@ A
{
�� 
_log
�� 
.
�� 
Info
�� !
(
��! "
response
��" *
.
��* +
Mensaje
��+ 2
)
��2 3
;
��3 4
}
�� 
else
�� 
{
�� 
_log
�� 
.
�� 
Warn
�� !
(
��! "
response
��" *
.
��* +
Mensaje
��+ 2
)
��2 3
;
��3 4
}
�� 
}
�� 
}
�� 
catch
�� 
(
�� 
	Exception
�� 
ex
�� 
)
��  
{
�� 
response
�� 
.
�� 
	IsExitoso
�� "
=
��# $
false
��% *
;
��* +
response
�� 
.
�� 
Mensaje
��  
=
��! "
$str
��# =
+
��> ?!
GetExceptionMessage
��@ S
(
��S T
ex
��T V
)
��V W
;
��W X
response
�� 
.
�� 

StatusCode
�� #
=
��$ %
HttpStatusCode
��& 4
.
��4 5

BadRequest
��5 ?
;
��? @
_log
�� 
.
�� 
Error
�� 
(
�� 
response
�� #
.
��# $
Mensaje
��$ +
)
��+ ,
;
��, -
}
�� 
return
�� 
response
�� 
;
�� 
}
�� 	
private
�� 
string
�� !
GetExceptionMessage
�� *
(
��* +
	Exception
��+ 4
	exception
��5 >
)
��> ?
{
�� 	
StringBuilder
�� 
	sbMessage
�� #
=
��$ %
new
��& )
StringBuilder
��* 7
(
��7 8
	exception
��8 A
.
��A B
Message
��B I
)
��I J
;
��J K
while
�� 
(
�� 
	exception
�� 
?
�� 
.
�� 
InnerException
�� ,
!=
��- /
null
��0 4
)
��4 5
{
�� 
	sbMessage
�� 
.
�� 
Append
��  
(
��  !
$str
��! $
+
��% &
	exception
��' 0
.
��0 1
InnerException
��1 ?
.
��? @
Message
��@ G
)
��G H
;
��H I
	exception
�� 
=
�� 
	exception
�� %
.
��% &
InnerException
��& 4
;
��4 5
}
�� 
return
�� 
	sbMessage
�� 
.
�� 
ToString
�� %
(
��% &
)
��& '
;
��' (
}
�� 	
}
�� 
}�� �
�D:\Banobras\Sistemas .NET\SST3\SIVARMER\Maincore2024\Riesgos.Simefin.WindowsService\Riesgos.Simefin.WindowsService.PortfolioLoad\Properties\AssemblyInfo.cs
[ 
assembly 	
:	 

AssemblyTitle 
( 
$str G
)G H
]H I
[		 
assembly		 	
:			 

AssemblyDescription		 
(		 
$str		 !
)		! "
]		" #
[

 
assembly

 	
:

	 
!
AssemblyConfiguration

  
(

  !
$str

! #
)

# $
]

$ %
[ 
assembly 	
:	 

AssemblyCompany 
( 
$str 
) 
] 
[ 
assembly 	
:	 

AssemblyProduct 
( 
$str I
)I J
]J K
[ 
assembly 	
:	 

AssemblyCopyright 
( 
$str 0
)0 1
]1 2
[ 
assembly 	
:	 

AssemblyTrademark 
( 
$str 
)  
]  !
[ 
assembly 	
:	 

AssemblyCulture 
( 
$str 
) 
] 
[ 
assembly 	
:	 


ComVisible 
( 
false 
) 
] 
[ 
assembly 	
:	 

Guid 
( 
$str 6
)6 7
]7 8
[## 
assembly## 	
:##	 

AssemblyVersion## 
(## 
$str## $
)##$ %
]##% &
[$$ 
assembly$$ 	
:$$	 

AssemblyFileVersion$$ 
($$ 
$str$$ (
)$$( )
]$$) *�
�D:\Banobras\Sistemas .NET\SST3\SIVARMER\Maincore2024\Riesgos.Simefin.WindowsService\Riesgos.Simefin.WindowsService.PortfolioLoad\ProjectInstaller.cs
	namespace 	
Riesgos
 
. 
Simefin 
. 
WindowsService (
.( )
PortfolioLoad) 6
{ 
[ 
RunInstaller 
( 
true 
) 
] 
public 

partial 
class 
ProjectInstaller )
:* +
	Installer, 5
{ 
public		 
ProjectInstaller		 
(		  
)		  !
{

 	
InitializeComponent 
(  
)  !
;! "
} 	
} 
} �d
�D:\Banobras\Sistemas .NET\SST3\SIVARMER\Maincore2024\Riesgos.Simefin.WindowsService\Riesgos.Simefin.WindowsService.PortfolioLoad\Program.cs
	namespace 	
Riesgos
 
. 
Simefin 
. 
WindowsService (
.( )
PortfolioLoad) 6
{ 
internal 
static 
class 
Program !
{ 
static 
void 
Main 
( 
string 
[  
]  !
args" &
)& '
{ 	
ServiceBase 
[ 
] 
ServicesToRun '
;' (
ServicesToRun 
= 
new 
ServiceBase  +
[+ ,
], -
{ 
new 
ServiceExcelLoad $
($ %
)% &
} 
; 
if 
( 
Environment 
. 
UserInteractive +
)+ ,
{ 
Console 
. 
	WriteLine !
(! "
$str" M
)M N
;N O
( 
( 
ServiceExcelLoad "
)" #
ServicesToRun# 0
[0 1
$num1 2
]2 3
)3 4
.4 5
Start5 :
(: ;
null; ?
)? @
;@ A
Console   
.   
ReadLine    
(    !
)  ! "
;  " #
(!! 
(!! 
ServiceExcelLoad!! "
)!!" #
ServicesToRun!!# 0
[!!0 1
$num!!1 2
]!!2 3
)!!3 4
.!!4 5
Stop!!5 9
(!!9 :
)!!: ;
;!!; <
}"" 
else## 
{$$ 
ServiceBase%% 
.%% 
Run%% 
(%%  
ServicesToRun%%  -
)%%- .
;%%. /
}&& 
}== 	
privateAA 
staticAA 
asyncAA 
TaskAA !
<AA! "
ApiResponseAA" -
>AA- .
GetTokenAA/ 7
(AA7 8
)AA8 9
{BB 	
ApiResponseCC 
responseCC  
=CC! "
newCC# &
ApiResponseCC' 2
{CC3 4
	IsExitosoCC5 >
=CC? @
falseCCA F
}CCG H
;CCH I
stringEE 
environmentVariableEE &
=EE' ( 
ConfigurationManagerEE) =
.EE= >
AppSettingsEE> I
[EEI J
$strEEJ _
]EE_ `
;EE` a
stringFF $
environmentVariableValueFF +
=FF, -
EnvironmentFF. 9
.FF9 :"
GetEnvironmentVariableFF: P
(FFP Q
environmentVariableFFQ d
)FFd e
;FFe f
ifGG 
(GG 
stringGG 
.GG 
IsNullOrEmptyGG $
(GG$ %$
environmentVariableValueGG% =
)GG= >
)GG> ?
{HH 
responseII 
.II 

StatusCodeII #
=II$ %
HttpStatusCodeII& 4
.II4 5

BadRequestII5 ?
;II? @
responseJJ 
.JJ 
MensajeJJ  
=JJ! "
$strJJ# E
;JJE F
returnKK 
responseKK 
;KK  
}LL 
varNN 
userDataNN 
=NN 
JsonConvertNN &
.NN& '
DeserializeObjectNN' 8
<NN8 9
TokenRequestNN9 E
>NNE F
(NNF G$
environmentVariableValueNNG _
)NN_ `
;NN` a
ifOO 
(OO 
stringOO 
.OO 
IsNullOrEmptyOO $
(OO$ %
userDataOO% -
.OO- .
UserNameOO. 6
)OO6 7
||OO8 :
stringOO; A
.OOA B
IsNullOrEmptyOOB O
(OOO P
userDataOOP X
.OOX Y
PasswordOOY a
)OOa b
)OOb c
{PP 
responseQQ 
.QQ 

StatusCodeQQ #
=QQ$ %
HttpStatusCodeQQ& 4
.QQ4 5

BadRequestQQ5 ?
;QQ? @
responseRR 
.RR 
MensajeRR  
=RR! "
$strRR# e
;RRe f
returnSS 
responseSS 
;SS  
}TT 
varVV 

parametersVV 
=VV 
newVV  
{VV! "
UserNameVV# +
=VV, -
userDataVV. 6
.VV6 7
UserNameVV7 ?
,VV? @
PasswordVVA I
=VVJ K
userDataVVL T
.VVT U
PasswordVVU ]
}VV^ _
;VV_ `
varWW 
jsonDataWW 
=WW 
JsonConvertWW &
.WW& '
SerializeObjectWW' 6
(WW6 7

parametersWW7 A
)WWA B
;WWB C
stringXX 
baseAddressXX 
=XX   
ConfigurationManagerXX! 5
.XX5 6
AppSettingsXX6 A
[XXA B
$strXXB N
]XXN O
;XXO P
stringYY 
endPointYY 
=YY  
ConfigurationManagerYY 2
.YY2 3
AppSettingsYY3 >
[YY> ?
$strYY? I
]YYI J
;YYJ K
try[[ 
{\\ 
using]] 
(]] 
var]] 
client]] !
=]]" #
new]]$ '

HttpClient]]( 2
(]]2 3
)]]3 4
)]]4 5
{^^ 
client__ 
.__ 
BaseAddress__ &
=__' (
new__) ,
Uri__- 0
(__0 1
baseAddress__1 <
)__< =
;__= >
client`` 
.`` !
DefaultRequestHeaders`` 0
.``0 1
Accept``1 7
.``7 8
Clear``8 =
(``= >
)``> ?
;``? @
clientaa 
.aa !
DefaultRequestHeadersaa 0
.aa0 1
Acceptaa1 7
.aa7 8
Addaa8 ;
(aa; <
newaa< ?+
MediaTypeWithQualityHeaderValueaa@ _
(aa_ `
$straa` r
)aar s
)aas t
;aat u
varcc 
contentcc 
=cc  !
newcc" %
StringContentcc& 3
(cc3 4
jsonDatacc4 <
,cc< =
Encodingcc> F
.ccF G
UTF8ccG K
,ccK L
$strccM _
)cc_ `
;cc` a
vardd 
resultdd 
=dd  
awaitdd! &
clientdd' -
.dd- .
	PostAsyncdd. 7
(dd7 8
endPointdd8 @
,dd@ A
contentddB I
)ddI J
;ddJ K
responseee 
=ee 
awaitee $
resultee% +
.ee+ ,
Contentee, 3
.ee3 4
ReadAsAsyncee4 ?
<ee? @
ApiResponseee@ K
>eeK L
(eeL M
)eeM N
;eeN O
}pp 
Consoleqq 
.qq 
	WriteLineqq !
(qq! "
responseqq" *
.qq* +
Mensajeqq+ 2
)qq2 3
;qq3 4
}rr 
catchss 
(ss 
	Exceptionss 
exss 
)ss  
{tt 
responseuu 
.uu 
	IsExitosouu "
=uu# $
falseuu% *
;uu* +
responsevv 
.vv 
Mensajevv  
=vv! "
$strvv# H
+vvI J
GetExceptionMessagevvK ^
(vv^ _
exvv_ a
)vva b
;vvb c
responseww 
.ww 

StatusCodeww #
=ww$ %
HttpStatusCodeww& 4
.ww4 5

BadRequestww5 ?
;ww? @
Consolexx 
.xx 
	WriteLinexx !
(xx! "
responsexx" *
.xx* +
Mensajexx+ 2
)xx2 3
;xx3 4
}yy 
return{{ 
response{{ 
;{{ 
}|| 	
private~~ 
static~~ 
async~~ 
Task~~ !
<~~! "
ApiResponse~~" -
>~~- .!
ExecuteLoadingProcess~~/ D
(~~D E
string~~E K
accessToken~~L W
)~~W X
{ 	
ApiResponse
�� 
response
��  
=
��! "
new
��# &
ApiResponse
��' 2
{
��3 4
	IsExitoso
��5 >
=
��? @
false
��A F
}
��G H
;
��H I
string
�� 
message
�� 
=
�� 
string
�� #
.
��# $
Empty
��$ )
;
��) *
string
�� 
baseAddress
�� 
=
��  "
ConfigurationManager
��! 5
.
��5 6
AppSettings
��6 A
[
��A B
$str
��B N
]
��N O
;
��O P
string
�� 
endPoint
�� 
=
�� "
ConfigurationManager
�� 2
.
��2 3
AppSettings
��3 >
[
��> ?
$str
��? M
]
��M N
;
��N O
try
�� 
{
�� 
using
�� 
(
�� 
var
�� 
client
�� !
=
��" #
new
��$ '

HttpClient
��( 2
(
��2 3
)
��3 4
)
��4 5
{
�� 
client
�� 
.
�� 
BaseAddress
�� &
=
��' (
new
��) ,
Uri
��- 0
(
��0 1
baseAddress
��1 <
)
��< =
;
��= >
client
�� 
.
�� #
DefaultRequestHeaders
�� 0
.
��0 1
Accept
��1 7
.
��7 8
Clear
��8 =
(
��= >
)
��> ?
;
��? @
client
�� 
.
�� #
DefaultRequestHeaders
�� 0
.
��0 1
Accept
��1 7
.
��7 8
Add
��8 ;
(
��; <
new
��< ?-
MediaTypeWithQualityHeaderValue
��@ _
(
��_ `
$str
��` r
)
��r s
)
��s t
;
��t u
client
�� 
.
�� #
DefaultRequestHeaders
�� 0
.
��0 1
Add
��1 4
(
��4 5
$str
��5 D
,
��D E
$str
��F O
+
��P Q
accessToken
��R ]
)
��] ^
;
��^ _
var
�� 
result
�� 
=
��  
await
��! &
client
��' -
.
��- .
	PostAsync
��. 7
(
��7 8
endPoint
��8 @
,
��@ A
null
��B F
)
��F G
;
��G H
response
�� 
=
�� 
await
�� $
result
��% +
.
��+ ,
Content
��, 3
.
��3 4
ReadAsAsync
��4 ?
<
��? @
ApiResponse
��@ K
>
��K L
(
��L M
)
��M N
;
��N O
}
�� 
Console
�� 
.
�� 
	WriteLine
�� !
(
��! "
response
��" *
.
��* +
Mensaje
��+ 2
)
��2 3
;
��3 4
}
�� 
catch
�� 
(
�� 
	Exception
�� 
ex
�� 
)
��  
{
�� 
response
�� 
.
�� 
	IsExitoso
�� "
=
��# $
false
��% *
;
��* +
response
�� 
.
�� 
Mensaje
��  
=
��! "
$str
��# Y
+
��Z [!
GetExceptionMessage
��\ o
(
��o p
ex
��p r
)
��r s
;
��s t
response
�� 
.
�� 

StatusCode
�� #
=
��$ %
HttpStatusCode
��& 4
.
��4 5

BadRequest
��5 ?
;
��? @
Console
�� 
.
�� 
	WriteLine
�� !
(
��! "
response
��" *
.
��* +
Mensaje
��+ 2
)
��2 3
;
��3 4
}
�� 
return
�� 
response
�� 
;
�� 
}
�� 	
private
�� 
static
�� 
string
�� !
GetExceptionMessage
�� 1
(
��1 2
	Exception
��2 ;
	exception
��< E
)
��E F
{
�� 	
string
�� 
message
�� 
=
�� 
	exception
�� &
.
��& '
Message
��' .
+
��/ 0
$str
��1 4
;
��4 5
while
�� 
(
�� 
	exception
�� 
?
�� 
.
�� 
InnerException
�� ,
!=
��- /
null
��0 4
)
��4 5
{
�� 
message
�� 
+=
�� 
	exception
�� $
.
��$ %
InnerException
��% 3
?
��3 4
.
��4 5
Message
��5 <
;
��< =
	exception
�� 
=
�� 
	exception
�� %
.
��% &
InnerException
��& 4
;
��4 5
}
�� 
return
�� 
message
�� 
;
�� 
}
�� 	
}
�� 
}�� �
�D:\Banobras\Sistemas .NET\SST3\SIVARMER\Maincore2024\Riesgos.Simefin.WindowsService\Riesgos.Simefin.WindowsService.PortfolioLoad\Models\TokenResponse.cs
	namespace 	
Riesgos
 
. 
Simefin 
. 
WindowsService (
.( )
PortfolioLoad) 6
.6 7
Models7 =
{ 
public 

class 
TokenResponse 
{ 
public		 
string		 
UserName		 
{		  
get		! $
;		$ %
set		& )
;		) *
}		+ ,
public 
string 
Nombre 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
AccessToken !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string 
RefreshToken "
{# $
get% (
;( )
set* -
;- .
}/ 0
} 
} �
�D:\Banobras\Sistemas .NET\SST3\SIVARMER\Maincore2024\Riesgos.Simefin.WindowsService\Riesgos.Simefin.WindowsService.PortfolioLoad\Models\TokenRequest.cs
	namespace 	
Riesgos
 
. 
Simefin 
. 
WindowsService (
.( )
PortfolioLoad) 6
.6 7
Models7 =
{ 
public 

class 
TokenRequest 
{ 
public		 
string		 
UserName		 
{		  
get		! $
;		$ %
set		& )
;		) *
}		+ ,
=		- .
string		/ 5
.		5 6
Empty		6 ;
;		; <
public 
string 
Password 
{  
get! $
;$ %
set& )
;) *
}+ ,
=- .
string/ 5
.5 6
Empty6 ;
;; <
} 
} �
�D:\Banobras\Sistemas .NET\SST3\SIVARMER\Maincore2024\Riesgos.Simefin.WindowsService\Riesgos.Simefin.WindowsService.PortfolioLoad\Models\ApiResponse.cs
	namespace 	
Riesgos
 
. 
Simefin 
. 
WindowsService (
.( )
PortfolioLoad) 6
.6 7
Models7 =
{ 
public

 

class

 
ApiResponse

 
{ 
public 
HttpStatusCode 

StatusCode (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public 
bool 
	IsExitoso 
{ 
get  #
;# $
set% (
;( )
}* +
=, -
false. 3
;3 4
public 
List 
< 
string 
> 
ErrorMessages )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
=8 9
new: =
List> B
<B C
stringC I
>I J
(J K
)K L
;L M
public 
object 
	Resultado 
{  !
get" %
;% &
set' *
;* +
}, -
public 
string 
Mensaje 
{ 
get  #
;# $
set% (
;( )
}* +
} 
} 