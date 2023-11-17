#!/bin/sh

published=none
for i in {1..3}; do
  online_package=$(nuget list Piiano.Vault)
  echo "Published: ${online_package}, Expecting: "
  published_ver=$(echo $online_package |  awk '{ print $2 }')
  if [ ${published_ver} != "1.0.6" ] ; then
    echo "New version is not ready yet - attempt ${i}"
    sleep 3
  else
    echo "Package is published"
    published=done
    break
  fi
done

if [ ${published} = "none" ] ; then
  echo "Nuget package is not published yet. Bailing out."
  exit 1
fi
