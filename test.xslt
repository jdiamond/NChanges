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
            </head>
            <body>
                <h1>
                    <xsl:value-of select="/assembly/@name" />
                    <xsl:text> </xsl:text>
                    <xsl:value-of select="/assembly/@version" />
                </h1>
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
        <xsl:apply-templates select="/assembly/type[@namespace = current()/@namespace][member/change]">
            <xsl:sort select="@name" />
        </xsl:apply-templates>
    </xsl:template>

    <xsl:template name="handle-namespace-with-changes">
        <!-- Context is /assembly/type that represents first type in namespace. -->
        <h2>Namespace <xsl:value-of select="@namespace" /></h2>
        <xsl:call-template name="process-types-in-namespace" />
    </xsl:template>

    <xsl:template name="handle-namespace-with-no-changes">
        <!-- Context is /assembly/type that represents first type in namespace. -->
    </xsl:template>

    <xsl:template match="type">
        <h3>
            <xsl:value-of select="@kind" />
            <xsl:text> </xsl:text>
            <xsl:value-of select="@name" />
        </h3>
        <xsl:variable name="type" select="." />
        <xsl:for-each select="member[not(preceding-sibling::member/change/@version = change/@version)]/change/@version">
            <xsl:sort select="." />
            <xsl:variable name="version" select="." />
            <h4><xsl:value-of select="." /></h4>
            <dl>
                <xsl:for-each select="$type/member[change/@version = $version]">
                    <xsl:sort select="@name" />
                    <dt>
                        <xsl:value-of select="@kind" />
                        <xsl:text> </xsl:text>
                        <xsl:value-of select="@name" />
                    </dt>
                    <xsl:apply-templates select="change[@version = $version]" />
                </xsl:for-each>
            </dl>
        </xsl:for-each>
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
        <code><xsl:value-of select="@new" /></code>
    </xsl:template>

    <xsl:template match="member/change" mode="handle-member-change" priority="0">
        <xsl:value-of select="@kind" />
    </xsl:template>

</xsl:transform>
