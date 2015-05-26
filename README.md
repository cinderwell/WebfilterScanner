# WebfilterScanner
An old project I made in C# to find secondary domains in a website that may need to be unblocked on a web filter

We were having an issue where our web filter could be blocking a secondary domain that a site was pulling resources from
(CSS, media,etc), so this tool was created to create a distinct list of those secondary domains by scanning the HTML of
the site. Then it would load each of those pages in separate web browsers and look for block messages.
