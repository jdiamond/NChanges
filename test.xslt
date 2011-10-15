<xsl:transform version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

    <xsl:output method="html" />

    <xsl:key name="types-by-namespace" match="/assembly/type" use="@namespace" />

    <xsl:template match="/">
        <html>
            <head>
                <title>
                    <xsl:value-of select="/assembly/@name" />
                    <xsl:text> </xsl:text>
                    <xsl:value-of select="/assembly/@version" />
                </title>
                <link rel="stylesheet" href="style.css" />
            </head>
            <body>
                <h1 class="assembly"><xsl:value-of select="/assembly/@name" /></h1>
                <xsl:call-template name="process-namespaces" />
            </body>
        </html>
    </xsl:template>

    <xsl:template name="process-namespaces">
        <!-- Context is /. -->
        <xsl:for-each select="/assembly/type[count(. | key('types-by-namespace', @namespace)[1]) = 1]">
            <xsl:sort select="@namespace" />
            <xsl:choose>
                <xsl:when test="/assembly/type[@namespace = current()/@namespace][change or member/change]">
                    <xsl:call-template name="handle-namespace-with-changes" />
                </xsl:when>
                <xsl:otherwise>
                    <xsl:call-template name="handle-namespace-with-no-changes" />
                </xsl:otherwise>
            </xsl:choose>
        </xsl:for-each>
    </xsl:template>

    <xsl:template name="process-types-in-namespace">
        <!-- Context is /assembly/type that represents first type in namespace. -->
        <xsl:apply-templates select="/assembly/type[@namespace = current()/@namespace][change or member/change]">
            <xsl:sort select="@name" />
        </xsl:apply-templates>
    </xsl:template>

    <xsl:template name="handle-namespace-with-changes">
        <!-- Context is /assembly/type that represents first type in namespace. -->
        <h2 class="namespace">Namespace <xsl:value-of select="@namespace" /></h2>
        <div style="margin-left: 1em;">
            <xsl:call-template name="process-types-in-namespace" />
        </div>
    </xsl:template>

    <xsl:template name="handle-namespace-with-no-changes">
        <!-- Context is /assembly/type that represents first type in namespace. -->
    </xsl:template>

    <xsl:template match="type[change]">
        <h3 class="type {@kind}">
            <xsl:value-of select="@kind" />
            <xsl:text> </xsl:text>
            <xsl:value-of select="@name" />
        </h3>
        <div style="margin-left: 1em;">
            <h4>Changes in <xsl:value-of select="change/@version" /></h4>
            <xsl:value-of select="change/@kind" />
        </div>
    </xsl:template>

    <xsl:template match="type">
        <h3 class="type {@kind}">
            <xsl:value-of select="@kind" />
            <xsl:text> </xsl:text>
            <xsl:value-of select="@name" />
        </h3>
        <div style="margin-left: 1em;">
            <xsl:variable name="type" select="." />
            <xsl:for-each select="member[not(preceding-sibling::member/change/@version = change/@version)]/change/@version">
                <xsl:sort select="." order="descending" />
                <xsl:variable name="version" select="." />
                <h4>Changes in <xsl:value-of select="$version" /></h4>
                <dl>
                    <xsl:apply-templates select="$type/member[change/@version = $version]">
                        <xsl:sort select="@name" />
                        <xsl:with-param name="version" select="$version" />
                    </xsl:apply-templates>
                </dl>
            </xsl:for-each>
        </div>
    </xsl:template>

    <xsl:template match="member">
        <xsl:param name="version" />
        <dt class="member {@kind}">
            <xsl:apply-templates select="." mode="describe-member" />
        </dt>
        <xsl:apply-templates select="change[@version = $version]" />
    </xsl:template>

    <xsl:template match="member[@kind='Method']" mode="describe-member">
        <xsl:value-of select="@name" />
        <xsl:text>(</xsl:text>
        <xsl:apply-templates select="param" mode="describe-param" />
        <xsl:text>)</xsl:text>
    </xsl:template>

    <xsl:template match="member" mode="describe-member">
        <xsl:value-of select="@name" />
    </xsl:template>

    <xsl:template match="param" mode="describe-param">
        <xsl:apply-templates select="@type" mode="describe-type" />
        <xsl:text> </xsl:text>
        <xsl:value-of select="@name" />
        <xsl:if test="position() &lt; last()">, </xsl:if>
    </xsl:template>

    <xsl:template match="@type" mode="describe-type">
        <xsl:call-template name="substring-after-last">
            <xsl:with-param name="haystack" select="." />
            <xsl:with-param name="needle" select="'.'" />
        </xsl:call-template>
    </xsl:template>

    <xsl:template name="substring-after-last">
        <xsl:param name="haystack" />
        <xsl:param name="needle" />
        <xsl:choose>
            <xsl:when test="contains($haystack, $needle)">
                <xsl:call-template name="substring-after-last">
                    <xsl:with-param name="haystack" select="substring-after($haystack, $needle)" />
                    <xsl:with-param name="needle" select="$needle" />
                </xsl:call-template>
            </xsl:when>
            <xsl:otherwise>
                <xsl:value-of select="$haystack" />
            </xsl:otherwise>
        </xsl:choose>
    </xsl:template>

    <xsl:template match="member/change">
        <dd>
            <xsl:apply-templates select="." mode="handle-member-change" />
        </dd>
    </xsl:template>

    <xsl:template match="member/change[@kind = 'AddedParameter']" mode="handle-member-change">
        <xsl:text>Added parameter </xsl:text>
        <code><xsl:value-of select="@new" /></code>
    </xsl:template>

    <xsl:template match="member/change[@kind = 'RemovedParameter']" mode="handle-member-change">
        <xsl:text>Removed parameter </xsl:text>
        <code><xsl:value-of select="@old" /></code>
    </xsl:template>

    <xsl:template match="member/change" mode="handle-member-change" priority="0">
        <xsl:value-of select="@kind" />
    </xsl:template>

</xsl:transform>
