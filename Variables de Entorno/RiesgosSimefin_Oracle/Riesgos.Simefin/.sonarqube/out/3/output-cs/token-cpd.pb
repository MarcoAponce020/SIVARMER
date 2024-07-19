ä
´D:\Banobras\Sistemas .NET\SST3\SIVARMER\TransicionNvaFSW\Trunk\C√≥digo\RiesgosSimefin_Oracle_2024_05_29\Riesgos.Simefin\Riesgos.Simefin.API\Helpers\StatusResponseHelper.cs
	namespace 	
Riesgos
 
. 
Simefin 
. 
WebAPI  
.  !
Helpers! (
{ 
public 

class  
StatusResponseHelper %
{		 
public 
static 
HttpResponseMessage )
GetStatusResponse* ;
(; <
ResponseDTO< G
responseH P
)P Q
{ 	
switch 
( 
response 
. 

StatusCode '
)' (
{ 
case 
HttpStatusCode #
.# $

BadRequest$ .
:. /
return 
( 
HttpResponseMessage /
)/ 0
Results0 7
.7 8

BadRequest8 B
(B C
responseC K
)K L
;L M
case 
HttpStatusCode #
.# $
OK$ &
:& '
return 
( 
HttpResponseMessage /
)/ 0
Results0 7
.7 8
Ok8 :
(: ;
response; C
)C D
;D E
case 
HttpStatusCode #
.# $
NotFound$ ,
:, -
return 
( 
HttpResponseMessage /
)/ 0
Results0 7
.7 8
NotFound8 @
(@ A
responseA I
)I J
;J K
default 
: 
return 
( 
HttpResponseMessage /
)/ 0
Results0 7
.7 8
Ok8 :
(: ;
); <
;< =
} 
}   	
}"" 
}$$ ﬁ8
¨D:\Banobras\Sistemas .NET\SST3\SIVARMER\TransicionNvaFSW\Trunk\C√≥digo\RiesgosSimefin_Oracle_2024_05_29\Riesgos.Simefin\Riesgos.Simefin.API\Controllers\UsuarioController.cs
	namespace 	
Riesgos
 
. 
Simefin 
. 
WebAPI  
.  !
Controllers! ,
{		 
[ 
Route 

(
 
$str 
) 
] 
[ 
ApiController 
] 
public 

class 
UsuarioController "
:# $
ControllerBase% 3
{ 
private 
readonly 
IUserUseCase %
_userUseCase& 2
;2 3
private 
readonly !
IAuthorizationUseCase .!
_authorizationUseCase/ D
;D E
public 
UsuarioController  
(  !
IUserUseCase! -
userUseCase. 9
,9 :!
IAuthorizationUseCase; P 
authorizationUseCaseQ e
)e f
{ 	
_userUseCase 
= 
userUseCase &
;& '!
_authorizationUseCase !
=" # 
authorizationUseCase$ 8
;8 9
} 	
[ 	
HttpPost	 
] 
[   	
Route  	 
(   
$str   
)   
]   
public!! 
async!! 
Task!! 
<!! 
IActionResult!! '
>!!' (
Login!!) .
(!!. /
[!!/ 0
FromBody!!0 8
]!!8 9
LoginRequestDTO!!: I
request!!J Q
)!!Q R
{"" 	
ResponseDTO## 
response##  
=##! "
new### &
ResponseDTO##' 2
(##2 3
)##3 4
;##4 5
if%% 
(%% 
!%% 

ModelState%% 
.%% 
IsValid%% #
)%%# $
{&& 
response'' 
.'' 

StatusCode'' #
=''$ %
HttpStatusCode''& 4
.''4 5

BadRequest''5 ?
;''? @
response(( 
.(( 
	IsExitoso(( "
=((# $
false((% *
;((* +
response)) 
.)) 
ErrorMessages)) &
.))& '
Add))' *
())* +
$str))+ N
)))N O
;))O P
return** 

BadRequest** !
(**! "
response**" *
)*** +
;**+ ,
}++ 
response-- 
=-- 
await-- 
_userUseCase-- )
.--) *
Login--* /
(--/ 0
request--0 7
)--7 8
;--8 9
return.. 
this.. 
... 
GetStatusResponse.. )
(..) *
response..* 2
)..2 3
;..3 4
}// 	
[66 	
HttpPost66	 
]66 
[77 	
Route77	 
(77 
$str77 
)77 
]77 
public88 
async88 
Task88 
<88 
IActionResult88 '
>88' ( 
GenerateRefreshToken88) =
(88= >
[88> ?
FromBody88? G
]88G H
TokenDTO88I Q
request88R Y
)88Y Z
{99 	
ResponseDTO:: 
response::  
=::! "
new::# &
ResponseDTO::' 2
(::2 3
)::3 4
;::4 5
if<< 
(<< 
!<< 

ModelState<< 
.<< 
IsValid<< #
)<<# $
{== 
response>> 
.>> 

StatusCode>> #
=>>$ %
HttpStatusCode>>& 4
.>>4 5

BadRequest>>5 ?
;>>? @
response?? 
.?? 
	IsExitoso?? "
=??# $
false??% *
;??* +
response@@ 
.@@ 
ErrorMessages@@ &
.@@& '
Add@@' *
(@@* +
$str@@+ W
)@@W X
;@@X Y
returnAA 

BadRequestAA !
(AA! "
responseAA" *
)AA* +
;AA+ ,
}BB 
responseDD 
=DD 
awaitDD !
_authorizationUseCaseDD 2
.DD2 3&
GenerateRefreshAccessTokenDD3 M
(DDM N
requestDDN U
)DDU V
;DDV W
returnFF 
thisFF 
.FF 
GetStatusResponseFF )
(FF) *
responseFF* 2
)FF2 3
;FF3 4
}GG 	
[OO 	
HttpPostOO	 
]OO 
[PP 	
RoutePP	 
(PP 
$strPP 
)PP 
]PP 
publicQQ 
asyncQQ 
TaskQQ 
<QQ 
IActionResultQQ '
>QQ' (
	RegistrarQQ) 2
(QQ2 3
[QQ3 4
FromBodyQQ4 <
]QQ< =
RegistroRequestDTOQQ> P
requestQQQ X
)QQX Y
{RR 	
ResponseDTOSS 
responseSS  
=SS! "
newSS# &
ResponseDTOSS' 2
(SS2 3
)SS3 4
;SS4 5
ifUU 
(UU 
!UU 

ModelStateUU 
.UU 
IsValidUU #
)UU# $
{VV 
responseWW 
.WW 
	IsExitosoWW "
=WW# $
falseWW% *
;WW* +
responseXX 
.XX 
ErrorMessagesXX &
.XX& '
AddXX' *
(XX* +
$strXX+ L
)XXL M
;XXM N
responseYY 
.YY 

StatusCodeYY #
=YY$ %
HttpStatusCodeYY& 4
.YY4 5

BadRequestYY5 ?
;YY? @
returnZZ 

BadRequestZZ !
(ZZ! "
responseZZ" *
)ZZ* +
;ZZ+ ,
}[[ 
response]] 
=]] 
await]] 
_userUseCase]] )
.]]) *
Register]]* 2
(]]2 3
request]]3 :
)]]: ;
;]]; <
return__ 
this__ 
.__ 
GetStatusResponse__ )
(__) *
response__* 2
)__2 3
;__3 4
}`` 	
privategg 
IActionResultgg 
GetStatusResponsegg /
(gg/ 0
ResponseDTOgg0 ;
responsegg< D
)ggD E
{hh 	
switchii 
(ii 
responseii 
.ii 

StatusCodeii '
)ii' (
{jj 
casekk 
HttpStatusCodekk #
.kk# $

BadRequestkk$ .
:kk. /
returnll 

BadRequestll %
(ll% &
responsell& .
)ll. /
;ll/ 0
casemm 
HttpStatusCodemm #
.mm# $
OKmm$ &
:mm& '
returnnn 
Oknn 
(nn 
responsenn &
)nn& '
;nn' (
caseoo 
HttpStatusCodeoo #
.oo# $
NotFoundoo$ ,
:oo, -
returnpp 
NotFoundpp #
(pp# $
responsepp$ ,
)pp, -
;pp- .
defaultqq 
:qq 
returnrr 
Okrr 
(rr 
)rr 
;rr  
}ss 
}tt 	
}vv 
}xx º\
ñD:\Banobras\Sistemas .NET\SST3\SIVARMER\TransicionNvaFSW\Trunk\C√≥digo\RiesgosSimefin_Oracle_2024_05_29\Riesgos.Simefin\Riesgos.Simefin.API\Program.cs
var 
builder 
= 
WebApplication 
. 
CreateBuilder *
(* +
args+ /
)/ 0
;0 1
ConfigurationManager 
configuration "
=# $
builder% ,
., -
Configuration- :
;: ;
builder 
. 
Services 
. 
AddSwaggerGen 
( 
options &
=>' )
{* +
options   
.   !
AddSecurityDefinition   !
(  ! "
$str  " *
,  * +
new  , /!
OpenApiSecurityScheme  0 E
{!! 
Description"" 
="" 
$str"" A
+""B C
$str## 4
,##4 5
Name$$ 
=$$ 
$str$$ 
,$$ 
In%% 

=%% 
ParameterLocation%% 
.%% 
Header%% %
,%%% &
Scheme&& 
=&& 
$str&& 
}'' 
)'' 
;'' 
options(( 
.(( "
AddSecurityRequirement(( "
(((" #
new((# &&
OpenApiSecurityRequirement((' A
(((A B
)((B C
{)) 
{** 	
new++ !
OpenApiSecurityScheme++ %
{,, 
	Reference-- 
=-- 
new-- 
OpenApiReference--  0
{.. 
Type// 
=// 
ReferenceType// (
.//( )
SecurityScheme//) 7
,//7 8
Id00 
=00 
$str00  
}11 
,11 
Scheme22 
=22 
$str22 !
,22! "
Name33 
=33 
$str33 
,33 
In44 
=44 
ParameterLocation44 &
.44& '
Header44' -
}55 
,55 
new66 
List66 
<66 
string66 
>66 
(66 
)66 
}77 	
}88 
)88 
;88 
}99 
)99 
;99 
Riesgos<< 
.<< 
Simefin<< 
.<< 
Infrastructure<< 
.<< 
Oracle<< %
.<<% &
Helpers<<& -
.<<- ."
OracleConnectionHelper<<. D
.<<D E

Initialize<<E O
(<<O P
configuration<<P ]
)<<] ^
;<<^ _
string?? 
key?? 

=?? 
string?? 
.?? 
Empty?? 
;?? 
string@@ 
environmentVariable@@ 
=@@ 
Environment@@ (
.@@( )"
GetEnvironmentVariable@@) ?
(@@? @
$str@@@ Z
)@@Z [
!@@[ \
;@@\ ]
ifAA 
(AA 
!AA 
stringAA 
.AA 
IsNullOrEmptyAA 
(AA 
environmentVariableAA -
)AA- .
)AA. /
{BB 
varCC 
dataCC 
=CC 
JsonConvertCC 
.CC 
DeserializeObjectCC ,
<CC, - 
EnvironmentVariablesCC- A
>CCA B
(CCB C
environmentVariableCCC V
!CCV W
)CCW X
;CCX Y
keyDD 
=DD 	
dataDD
 
!DD 
.DD 
SecretDD 
;DD 
}EE 
builderGG 
.GG 
ServicesGG 
.GG 
AddAuthenticationGG "
(GG" #
xGG# $
=>GG% '
{HH 
xII 
.II %
DefaultAuthenticateSchemeII 
=II  !
JwtBearerDefaultsII" 3
.II3 4 
AuthenticationSchemeII4 H
;IIH I
xJJ 
.JJ "
DefaultChallengeSchemeJJ 
=JJ 
JwtBearerDefaultsJJ 0
.JJ0 1 
AuthenticationSchemeJJ1 E
;JJE F
}KK 
)KK 
.LL 
AddJwtBearerLL 
(LL 
xLL 
=>LL 
{LL 
xMM 	
.MM	 
 
RequireHttpsMetadataMM
 
=MM  
falseMM! &
;MM& '
xNN 	
.NN	 

	SaveTokenNN
 
=NN 
trueNN 
;NN 
xOO 	
.OO	 
%
TokenValidationParametersOO
 #
=OO$ %
newOO& )%
TokenValidationParametersOO* C
{PP 	$
ValidateIssuerSigningKeyQQ $
=QQ% &
trueQQ' +
,QQ+ ,
IssuerSigningKeyRR 
=RR 
newRR " 
SymmetricSecurityKeyRR# 7
(RR7 8
EncodingRR8 @
.RR@ A
ASCIIRRA F
.RRF G
GetBytesRRG O
(RRO P
keyRRP S
!RRS T
)RRT U
)RRU V
,RRV W
ValidateIssuerSS 
=SS 
falseSS "
,SS" #
ValidateAudienceTT 
=TT 
falseTT $
,TT$ %
ValidateLifetimeUU 
=UU 
trueUU #
,UU# $
	ClockSkewVV 
=VV 
TimeSpanVV  
.VV  !
ZeroVV! %
}WW 	
;WW	 

}XX 
)XX 
;XX 
builder[[ 
.[[ 
Services[[ 
.[[ 
	AddScoped[[ 
<[[ !
IAuthorizationUseCase[[ 0
,[[0 1 
AuthorizationUseCase[[2 F
>[[F G
([[G H
)[[H I
;[[I J
builder\\ 
.\\ 
Services\\ 
.\\ 
	AddScoped\\ 
<\\ $
IAuthorizationRepository\\ 3
,\\3 4#
AuthorizationRepository\\5 L
>\\L M
(\\M N
)\\N O
;\\O P
builder]] 
.]] 
Services]] 
.]] 
	AddScoped]] 
<]] 
IUserUseCase]] '
,]]' (
UserUseCase]]) 4
>]]4 5
(]]5 6
)]]6 7
;]]7 8
builder^^ 
.^^ 
Services^^ 
.^^ 
	AddScoped^^ 
<^^ 
IUserRepository^^ *
,^^* +
UserRepository^^, :
>^^: ;
(^^; <
)^^< =
;^^= >
builder__ 
.__ 
Services__ 
.__ 
	AddScoped__ 
<__ 
IPortfolioUseCase__ ,
,__, -
PortfolioUseCase__. >
>__> ?
(__? @
)__@ A
;__A B
builder`` 
.`` 
Services`` 
.`` 
	AddScoped`` 
<``  
IPortfolioRepository`` /
,``/ 0
PortfolioRepository``1 D
>``D E
(``E F
)``F G
;``G H
builderpp 
.pp 
Servicespp 
.pp 
AddControllerspp 
(pp  
)pp  !
;pp! "
builderrr 
.rr 
Servicesrr 
.rr #
AddEndpointsApiExplorerrr (
(rr( )
)rr) *
;rr* +
builderss 
.ss 
Servicesss 
.ss 
AddSwaggerGenss 
(ss 
)ss  
;ss  !
varuu 
appuu 
=uu 	
builderuu
 
.uu 
Builduu 
(uu 
)uu 
;uu 
ifxx 
(xx 
appxx 
.xx 
Environmentxx 
.xx 
IsDevelopmentxx !
(xx! "
)xx" #
)xx# $
{yy 
appzz 
.zz 

UseSwaggerzz 
(zz 
)zz 
;zz 
app{{ 
.{{ 
UseSwaggerUI{{ 
({{ 
){{ 
;{{ 
}|| 
appÖÖ 
.
ÖÖ  
UseStatusCodePages
ÖÖ 
(
ÖÖ 
async
ÖÖ 
context
ÖÖ $
=>
ÖÖ% '
{ÜÜ 
if
áá 
(
áá 
context
áá 
.
áá 
HttpContext
áá 
.
áá 
Response
áá $
.
áá$ %

StatusCode
áá% /
==
áá0 2
(
áá3 4
int
áá4 7
)
áá7 8
HttpStatusCode
áá8 F
.
ááF G
Unauthorized
ááG S
)
ááS T
{
àà 
context
ââ 
.
ââ 
HttpContext
ââ 
.
ââ 
Response
ââ $
.
ââ$ %
Headers
ââ% ,
[
ââ, -
$str
ââ- ;
]
ââ; <
=
ââ= >
$str
ââ? Q
;
ââQ R
context
ää 
.
ää 
HttpContext
ää 
.
ää 
Response
ää $
.
ää$ %

StatusCode
ää% /
=
ää0 1
(
ää2 3
int
ää3 6
)
ää6 7
HttpStatusCode
ää7 E
.
ääE F
Unauthorized
ääF R
;
ääR S
await
åå 
context
åå 
.
åå 
HttpContext
åå !
.
åå! "
Response
åå" *
.
åå* +

WriteAsync
åå+ 5
(
åå5 6
System
åå6 <
.
åå< =
Text
åå= A
.
ååA B
Json
ååB F
.
ååF G
JsonSerializer
ååG U
.
ååU V
	Serialize
ååV _
(
åå_ `
new
çç 
{
éé 
	IsExitoso
èè 
=
èè 
false
èè !
,
èè! "

statusCode
êê 
=
êê 
$num
êê  
,
êê  !
Mensaje
ëë 
=
ëë 
$str
ëë r
}
íí 
)
ìì 	
)
ìì	 

;
ìì
 
}
ïï 
if
óó 
(
óó 
context
óó 
.
óó 
HttpContext
óó 
.
óó 
Response
óó $
.
óó$ %

StatusCode
óó% /
==
óó0 2
(
óó3 4
int
óó4 7
)
óó7 8
HttpStatusCode
óó8 F
.
óóF G
	Forbidden
óóG P
)
óóP Q
{
òò 
context
ôô 
.
ôô 
HttpContext
ôô 
.
ôô 
Response
ôô $
.
ôô$ %
Headers
ôô% ,
[
ôô, -
$str
ôô- ;
]
ôô; <
=
ôô= >
$str
ôô? Q
;
ôôQ R
context
öö 
.
öö 
HttpContext
öö 
.
öö 
Response
öö $
.
öö$ %

StatusCode
öö% /
=
öö0 1
(
öö2 3
int
öö3 6
)
öö6 7
HttpStatusCode
öö7 E
.
ööE F
	Forbidden
ööF O
;
ööO P
await
úú 
context
úú 
.
úú 
HttpContext
úú !
.
úú! "
Response
úú" *
.
úú* +

WriteAsync
úú+ 5
(
úú5 6
System
úú6 <
.
úú< =
Text
úú= A
.
úúA B
Json
úúB F
.
úúF G
JsonSerializer
úúG U
.
úúU V
	Serialize
úúV _
(
úú_ `
new
ùù 
{
ûû 
	IsExitoso
üü 
=
üü 
false
üü !
,
üü! "

statusCode
†† 
=
†† 
$num
††  
,
††  !
Mensaje
°° 
=
°° 
$str
°° a
}
¢¢ 
)
££ 	
)
££	 

;
££
 
}
§§ 
}•• 
)
•• 
;
•• 
appßß 
.
ßß !
UseHttpsRedirection
ßß 
(
ßß 
)
ßß 
;
ßß 
app©© 
.
©© 
UseAuthorization
©© 
(
©© 
)
©© 
;
©© 
app´´ 
.
´´ 
MapControllers
´´ 
(
´´ 
)
´´ 
;
´´ 
await≠≠ 
app
≠≠ 	
.
≠≠	 

RunAsync
≠≠
 
(
≠≠ 
)
≠≠ 
;
≠≠ ∞O
∞D:\Banobras\Sistemas .NET\SST3\SIVARMER\TransicionNvaFSW\Trunk\C√≥digo\RiesgosSimefin_Oracle_2024_05_29\Riesgos.Simefin\Riesgos.Simefin.API\Controllers\PortafoliosController.cs
	namespace

 	
Riesgos


 
.

 
Simefin

 
.

 
WebAPI

  
.

  !
Controllers

! ,
{ 
[ 
	Authorize 
] 
[ 
Route 

(
 
$str 
) 
] 
[ 
ApiController 
] 
public 

class !
PortafoliosController &
:' (
ControllerBase) 7
{ 
private 
readonly 
IPortfolioUseCase *
_portfolioUseCase+ <
;< =
private 
ResponseDTO 
	_response %
=& '
new( +
ResponseDTO, 7
(7 8
)8 9
;9 :
public !
PortafoliosController $
($ %
IPortfolioUseCase% 6
portfolioUseCase7 G
,G H
IConfigurationI W
configurationX e
)e f
{ 	
_portfolioUseCase 
= 
portfolioUseCase  0
;0 1
} 	
[!! 	
HttpGet!!	 
]!! 
public"" 
async"" 
Task"" 
<"" 
ActionResult"" &
<""& '
IEnumerable""' 2
<""2 3

Portafolio""3 =
>""= >
>""> ?
>""? @
GetAllPortfolios""A Q
(""Q R
)""R S
{## 	
var$$ 
portafolios$$ 
=$$ 
await$$ #
_portfolioUseCase$$$ 5
.$$5 6
GetAllPortfolio$$6 E
($$E F
)$$F G
;$$G H
return%% 
Ok%% 
(%% 
portafolios%% !
)%%! "
;%%" #
}&& 	
[-- 	
HttpGet--	 
(-- 
$str-- '
,--' (
Name--) -
=--. /
$str--0 D
)--D E
]--E F
public.. 
async.. 
Task.. 
<.. 
IActionResult.. '
>..' (
GetPortfolioByDate..) ;
(..; <
string..< B
fechaPosicion..C P
)..P Q
{// 	
if11 
(11 
fechaPosicion11 
==11  
null11! %
||11& (
fechaPosicion11) 6
==117 9
$str11: =
)11= >
{22 
	_response33 
.33 

StatusCode33 $
=33% &
HttpStatusCode33' 5
.335 6

BadRequest336 @
;33@ A
	_response44 
.44 
	IsExitoso44 #
=44$ %
false44& +
;44+ ,
	_response55 
.55 
ErrorMessages55 '
.55' (
Add55( +
(55+ ,
$str55, O
)55O P
;55P Q
return66 
this66 
.66 
GetStatusResponse66 -
(66- .
	_response66. 7
)667 8
;668 9
}77 
if99 
(99 
fechaPosicion99 
.99 
Length99 $
<99% &
$num99' (
)99( )
{:: 
	_response;; 
.;; 

StatusCode;; $
=;;% &
HttpStatusCode;;' 5
.;;5 6

BadRequest;;6 @
;;;@ A
	_response<< 
.<< 
	IsExitoso<< #
=<<$ %
false<<& +
;<<+ ,
	_response== 
.== 
ErrorMessages== '
.==' (
Add==( +
(==+ ,
$str	==, à
)
==à â
;
==â ä
return>> 
this>> 
.>> 
GetStatusResponse>> -
(>>- .
	_response>>. 7
)>>7 8
;>>8 9
}?? 
	_responseAA 
=AA 
awaitAA 
_portfolioUseCaseAA /
.AA/ 0
GetPortfolioByDateAA0 B
(AAB C
fechaPosicionAAC P
)AAP Q
;AAQ R
returnCC 
thisCC 
.CC 
GetStatusResponseCC )
(CC) *
	_responseCC* 3
)CC3 4
;CC4 5
}DD 	
[KK 	
HttpGetKK	 
(KK 
$strKK !
)KK! "
]KK" #
publicLL 
asyncLL 
TaskLL 
<LL 
IActionResultLL '
>LL' (
GetPortfolioByIdLL) 9
(LL9 :
intLL: =
idPortafolioLL> J
)LLJ K
{MM 	
	_responseNN 
=NN 
awaitNN 
_portfolioUseCaseNN /
.NN/ 0
GetPortfolioByIdNN0 @
(NN@ A
idPortafolioNNA M
)NNM N
;NNN O
returnOO 
thisOO 
.OO 
GetStatusResponseOO )
(OO) *
	_responseOO* 3
)OO3 4
;OO4 5
}PP 	
[dd 	
HttpPutdd	 
(dd 
$strdd !
)dd! "
]dd" #
publicee 
asyncee 
Taskee 
<ee 
IActionResultee '
>ee' (
UpdatePortfolioee) 8
(ee8 9
intee9 <
idPortafolioee= I
,eeI J
PortafolioUpdateDTOeeK ^
requestee_ f
)eef g
{ff 	
ifgg 
(gg 
idPortafoliogg 
==gg 
$numgg  !
||gg" $
(gg% &
requestgg& -
==gg. 0
nullgg1 5
||gg6 8
stringgg9 ?
.gg? @
IsNullOrEmptygg@ M
(ggM N
requestggN U
.ggU V

ListaDatosggV `
)gg` a
)gga b
)ggb c
{hh 
	_responseii 
.ii 
	IsExitosoii #
=ii$ %
falseii& +
;ii+ ,
	_responsejj 
.jj 

StatusCodejj $
=jj% &
HttpStatusCodejj' 5
.jj5 6

BadRequestjj6 @
;jj@ A
thiskk 
.kk 
GetStatusResponsekk &
(kk& '
	_responsekk' 0
)kk0 1
;kk1 2
}ll 
	_responsenn 
=nn 
awaitnn 
_portfolioUseCasenn /
.nn/ 0
UpdatePortfolionn0 ?
(nn? @
idPortafolionn@ L
,nnL M
requestnnN U
!nnU V
)nnV W
;nnW X
returnpp 
thispp 
.pp 
GetStatusResponsepp )
(pp) *
	_responsepp* 3
)pp3 4
;pp4 5
}qq 	
[xx 	

HttpDeletexx	 
(xx 
$strxx $
)xx$ %
]xx% &
publicyy 
asyncyy 
Taskyy 
<yy 
IActionResultyy '
>yy' (
DeletePortfolioyy) 8
(yy8 9
intyy9 <
idPortafolioyy= I
)yyI J
{zz 	
	_response{{ 
={{ 
await{{ 
_portfolioUseCase{{ /
.{{/ 0
DeletePortfolio{{0 ?
({{? @
idPortafolio{{@ L
){{L M
;{{M N
return|| 
this|| 
.|| 
GetStatusResponse|| )
(||) *
	_response||* 3
)||3 4
;||4 5
}}} 	
[
ÑÑ 	
HttpPost
ÑÑ	 
(
ÑÑ 
$str
ÑÑ 
)
ÑÑ 
]
ÑÑ 
public
ÖÖ 
async
ÖÖ 
Task
ÖÖ 
<
ÖÖ 
IActionResult
ÖÖ '
>
ÖÖ' (
	ExcelLoad
ÖÖ) 2
(
ÖÖ2 3
	IFormFile
ÖÖ3 <
?
ÖÖ< =
file
ÖÖ> B
)
ÖÖB C
{
ÜÜ 	
	_response
áá 
=
áá 
await
áá 
_portfolioUseCase
áá /
.
áá/ 0
	ExcelLoad
áá0 9
(
áá9 :
file
áá: >
!
áá> ?
)
áá? @
;
áá@ A
return
àà 
this
àà 
.
àà 
GetStatusResponse
àà )
(
àà) *
	_response
àà* 3
)
àà3 4
;
àà4 5
}
ââ 	
[
êê 	
HttpGet
êê	 
(
êê 
$str
êê "
,
êê" #
Name
êê$ (
=
êê) *
$str
êê+ 7
)
êê7 8
]
êê8 9
public
ëë 
async
ëë 
Task
ëë 
<
ëë 
IActionResult
ëë '
>
ëë' (

GetFileCSV
ëë) 3
(
ëë3 4
string
ëë4 :
fileDate
ëë; C
)
ëëC D
{
íí 	
	_response
ìì 
=
ìì 
await
ìì 
_portfolioUseCase
ìì /
.
ìì/ 0

GetFileCSV
ìì0 :
(
ìì: ;
fileDate
ìì; C
)
ììC D
;
ììD E
return
îî 
this
îî 
.
îî 
GetStatusResponse
îî )
(
îî) *
	_response
îî* 3
)
îî3 4
;
îî4 5
}
ïï 	
private
úú 
IActionResult
úú 
GetStatusResponse
úú /
(
úú/ 0
ResponseDTO
úú0 ;
response
úú< D
)
úúD E
{
ùù 	
switch
ûû 
(
ûû 
response
ûû 
.
ûû 

StatusCode
ûû '
)
ûû' (
{
üü 
case
†† 
HttpStatusCode
†† #
.
††# $

BadRequest
††$ .
:
††. /
return
°° 

BadRequest
°° %
(
°°% &
response
°°& .
)
°°. /
;
°°/ 0
case
¢¢ 
HttpStatusCode
¢¢ #
.
¢¢# $
OK
¢¢$ &
:
¢¢& '
return
££ 
Ok
££ 
(
££ 
response
££ &
)
££& '
;
££' (
case
§§ 
HttpStatusCode
§§ #
.
§§# $
NotFound
§§$ ,
:
§§, -
return
•• 
NotFound
•• #
(
••# $
response
••$ ,
)
••, -
;
••- .
default
¶¶ 
:
¶¶ 
return
ßß 
Ok
ßß 
(
ßß 
)
ßß 
;
ßß  
}
®® 
}
©© 	
}
¨¨ 
}ÆÆ 